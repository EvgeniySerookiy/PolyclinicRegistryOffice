namespace PolyclinicRegistryOffice.Entities;

public class ScheduleSlot
{
    // Первичный ключ (Primary Key)
    public int Id { get; set; }

    // Внешний ключ (Foreign Key)
    public int SpecialistId { get; set; }
    
    // Основные данные
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; } // Время начала
    public TimeSpan EndTime { get; set; } // Время окончания
    public int DurationMinutes { get; set; } // Длительность приема в минутах

    // Навигационное свойство (Связи: Слот -> Один Специалист)
    public Specialist Specialist { get; set; }
}