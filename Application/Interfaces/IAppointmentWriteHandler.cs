using PolyclinicRegistryOffice.Application.Handlers.Appointment.Write.Command;
using PolyclinicRegistryOffice.Entities;
using PolyclinicRegistryOffice.ErrorContext;

namespace PolyclinicRegistryOffice.Application.Interfaces;

public interface IAppointmentWriteHandler
{
    Task<Result<Appointment, Error>> BookAppointment(
        BookAppointmentCommand command, 
        CancellationToken cancellationToken);
}