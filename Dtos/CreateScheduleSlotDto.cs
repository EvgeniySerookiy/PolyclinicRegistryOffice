namespace PolyclinicRegistryOffice.Dtos;

public record CreateScheduleSlotDto(
    int SpecialistId,
    DateTime Date,
    TimeSpan StartTime,
    TimeSpan EndTime,
    int DurationMinutes);