using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PolyclinicRegistryOffice.Application.Handlers.WriteHandlers.Appointment.Write.Command;
using PolyclinicRegistryOffice.Application.Interfaces.WriteInterfaces;
using PolyclinicRegistryOffice.Data;
using PolyclinicRegistryOffice.Entities;
using PolyclinicRegistryOffice.ErrorContext;

namespace PolyclinicRegistryOffice.Application.Handlers.WriteHandlers.Appointment.Write;

// Используем IValidator<Appointment> для валидации данных записи
public class AppointmentWriteHandler(
    WriteDbContext context, 
    IValidator<Entities.Appointment> appointmentValidator,
    IValidator<Entities.Patient> patientValidator) : IAppointmentWriteHandler
{
    public async Task<Result<Entities.Appointment, Error>> BookAppointment(
        BookAppointmentCommand command, 
        CancellationToken cancellationToken)
    {
        // 1. НАЙТИ ИЛИ СОЗДАТЬ ПАЦИЕНТА
        
        // Попытка найти существующего пациента по Email и ФИО (пример эвристики)
        var patient = await context.Patients
            .FirstOrDefaultAsync(p => 
                p.Email == command.PatientEmail && 
                p.PhoneNumber == command.PatientPhoneNumber, 
                cancellationToken);

        if (patient == null)
        {
            // Если не найден, создаем новый объект Patient
            patient = new Entities.Patient
            {
                LastName = command.PatientLastName,
                FirstName = command.PatientFirstName,
                MiddleName = command.PatientMiddleName,
                PhoneNumber = command.PatientPhoneNumber,
                Email = command.PatientEmail,
                DateOfBirth = command.PatientDateOfBirth
            };
            
            // Валидация данных нового пациента
            var patientValidationResult = await patientValidator.ValidateAsync(patient, cancellationToken);
            if (!patientValidationResult.IsValid)
            {
                // Возвращаем ошибку с типом Success=Appointment
                return Result<Entities.Appointment, Error>.Failure(ValidationErrorMapper.ToSingleError(patientValidationResult.Errors));
            }

            context.Patients.Add(patient);
            await context.SaveChangesAsync(cancellationToken);
        }
        
        // 2. ПРОВЕРКА СПЕЦИАЛИСТА И ДОСТУПНОСТИ ВРЕМЕНИ

        // Проверка существования Специалиста
        var specialist = await context.Specialists
            .FindAsync(new object[] { command.SpecialistId }, cancellationToken);

        if (specialist == null)
        {
            return Result<Entities.Appointment, Error>.Failure(Errors.Specialist.NotFound(command.SpecialistId));
        }

        // Проверка, занят ли слот (например, в БД уже есть другая запись)
        var conflict = await context.Appointments
            .AnyAsync(a => 
                a.SpecialistId == command.SpecialistId &&
                a.AppointmentDate == command.AppointmentDate.Date && // Сравниваем только дату
                a.AppointmentTime == command.AppointmentTime && // Сравниваем точное время начала
                a.Status == AppointmentStatus.Booked, // Только активные записи
                cancellationToken);

        if (conflict)
        {
            return Result<Entities.Appointment, Error>.Failure(Errors.Appointment.Conflict());
        }

        // *Здесь может быть дополнительная логика проверки расписания Specialist.ScheduleSlot*

        // 3. СОЗДАНИЕ ЗАПИСИ
        
        var appointment = new Entities.Appointment
        {
            PatientId = patient.Id, // Используем найденный/созданный Patient ID
            SpecialistId = command.SpecialistId,
            AppointmentDate = command.AppointmentDate.Date,
            AppointmentTime = command.AppointmentTime,
            Status = AppointmentStatus.Booked
        };
        
        // Валидация самой записи (например, дата не в прошлом)
        var appointmentValidationResult = await appointmentValidator.ValidateAsync(appointment, cancellationToken);
        if (!appointmentValidationResult.IsValid)
        {
            return Result<Entities.Appointment, Error>.Failure(ValidationErrorMapper.ToSingleError(appointmentValidationResult.Errors));
        }

        context.Appointments.Add(appointment);
        await context.SaveChangesAsync(cancellationToken);
        
        return Result<Entities.Appointment, Error>.Success(appointment);
    }
}