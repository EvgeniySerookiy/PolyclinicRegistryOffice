namespace PolyclinicRegistryOffice.Entities;

public class Feedback
{
    // Первичный ключ (Primary Key)
    public int Id { get; set; }

    // Основные данные
    public string LastName { get; set; }  
    public string FirstName { get; set; } 
    public string MiddleName { get; set; } // Имя отправителя
    public string Email { get; set; } // Email
    public string Message { get; set; } // Текст сообщения
    public DateTime Date { get; set; } // Дата/время отправки
    public FeedbackStatus Status { get; set; } = FeedbackStatus.New;
}