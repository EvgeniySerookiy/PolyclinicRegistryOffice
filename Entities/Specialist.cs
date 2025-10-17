using System.Text.Json.Serialization;

namespace PolyclinicRegistryOffice.Entities;

public class Specialist
{
    // Первичный ключ (Primary Key)
    public int Id { get; set; }

    // Основные данные
    public string LastName { get; set; }  
    public string FirstName { get; set; } 
    public string MiddleName { get; set; }
    public Specialization Specialization { get; set; }
    public string OfficeNumber { get; set; } // Кабинет
    public string Description { get; set; } // Краткое описание

    // Навигационные свойства (Связи: Один Специалист -> Много Записей)
    [JsonIgnore]
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    [JsonIgnore]
    public ICollection<ScheduleSlot> ScheduleSlots { get; set; } = new List<ScheduleSlot>();
}