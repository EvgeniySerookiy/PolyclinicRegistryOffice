using PolyclinicRegistryOffice.Application.Handlers.Appointment.Write.Command;

namespace PolyclinicRegistryOffice.Controllers.Write.WriteAppointment;

public record BookAppointmentRequest(
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
    TimeSpan AppointmentTime)
{
    public BookAppointmentCommand ToCommand()
    {
        return new BookAppointmentCommand(
            PatientLastName, 
            PatientFirstName, 
            PatientMiddleName, 
            PatientPhoneNumber,
            PatientEmail,
            PatientDateOfBirth,
            SpecialistId,
            AppointmentDate,
            AppointmentTime);
    }
}