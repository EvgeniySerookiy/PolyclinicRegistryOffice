using PolyclinicRegistryOffice.Application.Handlers.WriteHandlers.Appointment.Write.Command;
using PolyclinicRegistryOffice.Entities;
using PolyclinicRegistryOffice.ErrorContext;

namespace PolyclinicRegistryOffice.Application.Interfaces.WriteInterfaces;

public interface IAppointmentWriteHandler
{
    Task<Result<Appointment, Error>> BookAppointment(
        BookAppointmentCommand command, 
        CancellationToken cancellationToken);
}