public record GetFreeSlotDto
{
    // Пустой конструктор для Dapper. 
    // Поскольку это record, он не строго обязателен, но помогает Dapper.
    public GetFreeSlotDto() {} 

    // Основной конструктор, используемый в логике генерации свободных слотов
    public GetFreeSlotDto(int id, DateTime date, TimeSpan startTime, TimeSpan endTime, int durationMinutes)
    {
        Id = id;
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        DurationMinutes = durationMinutes;
    }

    // Свойства должны точно соответствовать именам, ожидаемым Dapper или вашей логикой
    public int Id { get; init; }
    public DateTime Date { get; init; }
    public TimeSpan StartTime { get; init; }
    public TimeSpan EndTime { get; init; }
    public int DurationMinutes { get; init; }
}