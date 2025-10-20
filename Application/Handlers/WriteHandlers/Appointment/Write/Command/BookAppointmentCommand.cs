namespace PolyclinicRegistryOffice.Application.Handlers.WriteHandlers.Appointment.Write.Command;

public record BookAppointmentCommand(
    // Данные Пациента (для поиска/создания сущности Patient)
    string PatientLastName,
    string PatientFirstName,
    string PatientMiddleName,
    string PatientPhoneNumber,
    string PatientEmail,
    DateTime PatientDateOfBirth,
    
    // Данные Записи (Appointment)
    int SpecialistId,
    DateTime AppointmentDate,
    TimeSpan AppointmentTime);