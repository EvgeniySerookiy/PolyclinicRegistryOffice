namespace PolyclinicRegistryOffice.Entities;
 
 public class Patient
 {
     // Первичный ключ (Primary Key)
     public int Id { get; set; }
 
     // Основные данные
     public string LastName { get; set; }  
     public string FirstName { get; set; } 
     public string MiddleName { get; set; }
     public string PhoneNumber { get; set; } // Телефон
     public string Email { get; set; }
     public DateTime DateOfBirth { get; set; } // Дата рождения
 
     // Навигационные свойства (Связи: Один Пациент -> Много Записей)
     public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
 }