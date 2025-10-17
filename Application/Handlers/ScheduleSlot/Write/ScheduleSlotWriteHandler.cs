using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PolyclinicRegistryOffice.Application.Handlers.ScheduleSlot.Write.Command;
using PolyclinicRegistryOffice.Application.Interfaces;
using PolyclinicRegistryOffice.Data;
using PolyclinicRegistryOffice.Dtos;
using PolyclinicRegistryOffice.ErrorContext;

namespace PolyclinicRegistryOffice.Application.Handlers.ScheduleSlot.Write;

public class ScheduleSlotWriteHandler(
    WriteDbContext context,
    IValidator<Entities.ScheduleSlot> validator) : IScheduleSlotWriteHandler
{
    public async Task<Result<Entities.ScheduleSlot, Error>> CreateScheduleSlot(
        CreateScheduleSlotDto command,
        CancellationToken cancellationToken)
    {
        var slot = new Entities.ScheduleSlot
        {
            SpecialistId = command.SpecialistId,
            Date = command.Date.Date,
            StartTime = command.StartTime,
            EndTime = command.EndTime,
            DurationMinutes = command.DurationMinutes
        };

        var validationResult = await validator.ValidateAsync(slot, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<Entities.ScheduleSlot, Error>.Failure(
                ValidationErrorMapper.ToSingleError(validationResult.Errors));
        }

        var specialistExists = await context.Specialists
            .AnyAsync(s => s.Id == command.SpecialistId, cancellationToken);

        if (!specialistExists)
        {
            return Result<Entities.ScheduleSlot, Error>.Failure(Errors.Specialist.NotFound(command.SpecialistId));
        }

        if (command.StartTime >= command.EndTime)
        {
            return Result<Entities.ScheduleSlot, Error>.Failure(
                Error.Validation("slot.time.invalid", "Start time must be strictly before end time.")
            );
        }

        var conflict = await context.ScheduleSlots
            .AnyAsync(s =>
                    s.SpecialistId == command.SpecialistId &&
                    s.Date.Date == command.Date.Date &&
                    (
                        (command.StartTime < s.EndTime && command.EndTime > s.StartTime)
                        || (s.StartTime < command.EndTime && s.EndTime > command.StartTime)
                    ),
                cancellationToken);

        if (conflict)
        {
            return Result<Entities.ScheduleSlot, Error>.Failure(
                Errors.ScheduleSlot.Conflict("The requested time slot overlaps with an existing schedule slot."));
        }

        context.ScheduleSlots.Add(slot);
        await context.SaveChangesAsync(cancellationToken);

        return Result<Entities.ScheduleSlot, Error>.Success(slot);
    }

    public async Task<Result<List<Entities.ScheduleSlot>, Error>> CreateScheduleSlots(
        CreateScheduleSlotsCommand command,
        CancellationToken cancellationToken)
    {
        if (command.Slots == null || !command.Slots.Any())
        {
            return Result<List<Entities.ScheduleSlot>, Error>.Failure(Errors.ScheduleSlot.BulkEmpty());
        }

        var slotsToSave = new List<Entities.ScheduleSlot>();
        
        var firstSlot = command.Slots.First();
        var specialistExists = await context.Specialists
            .AnyAsync(s => s.Id == firstSlot.SpecialistId, cancellationToken);

        if (!specialistExists)
        {
            return Result<List<Entities.ScheduleSlot>, Error>.Failure(
                Errors.Specialist.NotFound(firstSlot.SpecialistId));
        }
        
        foreach (var slotCommand in command.Slots)
        {
            var slot = new Entities.ScheduleSlot
            {
                SpecialistId = slotCommand.SpecialistId,
                Date = slotCommand.Date.Date,
                StartTime = slotCommand.StartTime,
                EndTime = slotCommand.EndTime,
                DurationMinutes = slotCommand.DurationMinutes
            };
            
            var validationResult = await validator.ValidateAsync(slot, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result<List<Entities.ScheduleSlot>, Error>.Failure(
                    ValidationErrorMapper.ToSingleError(validationResult.Errors)
                );
            }
            
            if (slotCommand.StartTime >= slotCommand.EndTime)
            {
                return Result<List<Entities.ScheduleSlot>, Error>.Failure(
                    Error.Validation("slot.time.invalid",
                        $"Start time must be strictly before end time for slot on {slotCommand.Date.ToShortDateString()}.")
                );
            }
            
            var conflictWithDb = await context.ScheduleSlots
                .AnyAsync(s =>
                        s.SpecialistId == slotCommand.SpecialistId &&
                        s.Date.Date == slotCommand.Date.Date &&
                        (
                            (slotCommand.StartTime < s.EndTime && slotCommand.EndTime > s.StartTime)
                            || (s.StartTime < slotCommand.EndTime && s.EndTime > slotCommand.StartTime)
                        ),
                    cancellationToken);

            if (conflictWithDb)
            {
                return Result<List<Entities.ScheduleSlot>, Error>.Failure(
                    Errors.ScheduleSlot.Conflict(
                        $"Slot on {slotCommand.Date.ToShortDateString()} from {slotCommand.StartTime} conflicts with an existing slot in the database.")
                );
            }
            
            var conflictWithBatch = slotsToSave
                .Any(s =>
                    s.Date.Date == slotCommand.Date.Date &&
                    (
                        (slotCommand.StartTime < s.EndTime && slotCommand.EndTime > s.StartTime)
                        || (s.StartTime < slotCommand.EndTime && s.EndTime > slotCommand.StartTime)
                    ));

            if (conflictWithBatch)
            {
                return Result<List<Entities.ScheduleSlot>, Error>.Failure(
                    Errors.ScheduleSlot.Conflict(
                        $"Slot on {slotCommand.Date.ToShortDateString()} from {slotCommand.StartTime} conflicts with another slot in this batch.")
                );
            }

            slotsToSave.Add(slot);
        }
        
        context.ScheduleSlots.AddRange(slotsToSave);
        await context.SaveChangesAsync(cancellationToken);

        return Result<List<Entities.ScheduleSlot>, Error>.Success(slotsToSave);
    }
}