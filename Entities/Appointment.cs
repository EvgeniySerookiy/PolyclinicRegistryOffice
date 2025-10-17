namespace PolyclinicRegistryOffice.Entities;

public class Appointment
{
    // Первичный ключ (Primary Key)
    public int Id { get; set; }

    // Внешние ключи (Foreign Keys)
    public int PatientId { get; set; }
    public int SpecialistId { get; set; }

    // Основные данные
    public DateTime AppointmentDate { get; set; }
    public TimeSpan AppointmentTime { get; set; } 
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Booked;

    // Навигационные свойства (Связи: Запись -> Один Пациент, Запись -> Один Специалист)
    public Patient Patient { get; set; }
    public Specialist Specialist { get; set; }
}