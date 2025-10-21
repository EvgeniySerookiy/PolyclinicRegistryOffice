using Dapper;
using PolyclinicRegistryOffice.Application.Interfaces.ReadInterfaces;
using PolyclinicRegistryOffice.ErrorContext;
using PolyclinicRegistryOffice.Interfaces;

namespace PolyclinicRegistryOffice.Application.Handlers.ReadHandlers.ScheduleSlot.Read;

public class ScheduleSlotReadHandler(
    ISqlConnectionFactory sqlConnectionFactory) : IScheduleSlotReadHandler
{
    public async Task<Result<IEnumerable<GetFreeSlotDto>, Error>> GetFreeSlotsBySpecialist(
        int specialistId,
        CancellationToken cancellationToken)
    {
        try
        {
            using var connection = sqlConnectionFactory.Connection();

            // 1️⃣ Получаем исходные широкие слоты расписания, используя анонимный тип.
            const string sqlSlots = @"
                SELECT 
                    ss.""Id"" AS ""ScheduleId"",
                    ss.""Date"",
                    ss.""StartTime"",
                    ss.""EndTime"",
                    ss.""DurationMinutes""
                FROM ""ScheduleSlots"" ss
                WHERE ss.""SpecialistId"" = @SpecialistId
                  -- Убираем прошедшие слоты. Важно: Date + StartTime должно быть больше текущего момента (UTC)
                  AND (ss.""Date"" + ss.""StartTime"") > NOW() AT TIME ZONE 'UTC' 
                ORDER BY ss.""Date"", ss.""StartTime"";";
            
            var scheduleSlots = (await connection.QueryAsync<
                // Анонимный тип для получения данных из DB
                dynamic>(
                new CommandDefinition(sqlSlots, new { SpecialistId = specialistId }, cancellationToken: cancellationToken)
            )).Select(s => new 
            {
                ScheduleId = (int)s.ScheduleId,
                Date = (DateTime)s.Date, // Явно приводим типы
                StartTime = (TimeSpan)s.StartTime,
                EndTime = (TimeSpan)s.EndTime,
                DurationMinutes = (int)s.DurationMinutes
            }).ToList();

            // 2️⃣ Получаем список дат для поиска занятых слотов
            var scheduleDates = scheduleSlots.Select(s => s.Date.Date).Distinct().ToList();
            
            if (!scheduleDates.Any())
            {
                return Result<IEnumerable<GetFreeSlotDto>, Error>.Success(Enumerable.Empty<GetFreeSlotDto>());
            }

            // 3️⃣ Получаем уже занятые времена только для актуальных дат
            const string sqlAppointments = @"
                SELECT 
                    a.""AppointmentDate"" AS ""Date"",
                    a.""AppointmentTime"" AS ""StartTime""
                FROM ""Appointments"" a
                WHERE a.""SpecialistId"" = @SpecialistId
                  AND a.""AppointmentDate"" = ANY(@Dates) -- Ограничиваем поиск датами расписания
                  AND a.""Status"" = 0; -- Booked
            ";

            var takenAppointments = (await connection.QueryAsync<(DateTime Date, TimeSpan StartTime)>(
                new CommandDefinition(sqlAppointments, 
                                      new { SpecialistId = specialistId, Dates = scheduleDates }, 
                                      cancellationToken: cancellationToken)
            )).ToList();

            // 4️⃣ Генерация свободных интервалов
            var freeSlots = new List<GetFreeSlotDto>();

            foreach (var s in scheduleSlots)
            {
                var start = s.StartTime;
                while (start < s.EndTime)
                {
                    var slotEnd = start.Add(TimeSpan.FromMinutes(s.DurationMinutes));

                    // Создаем DateTime для точного сравнения с NOW()
                    var slotDateTime = s.Date.Date.Add(start);
                    
                    // Пропускаем слоты, которые уже начались
                    if (slotDateTime <= DateTime.UtcNow)
                    {
                         start = slotEnd;
                         continue;
                    }

                    // Проверяем, занят ли данный интервал
                    var isTaken = takenAppointments.Any(t =>
                        // Сравниваем только дату (без времени)
                        t.Date.Date == s.Date.Date && 
                        // Сравниваем время
                        t.StartTime == start
                    );

                    if (!isTaken)
                    {
                        // Создаем DTO для свободного интервала записи
                        freeSlots.Add(new GetFreeSlotDto(
                            s.ScheduleId,
                            s.Date,
                            start,
                            slotEnd,
                            s.DurationMinutes
                        ));
                    }

                    start = slotEnd;
                }
            }

            return Result<IEnumerable<GetFreeSlotDto>, Error>.Success(freeSlots);
        }
        catch (Exception ex)
        {
            // Убедитесь, что вы правильно логируете ошибку ex
            return Result<IEnumerable<GetFreeSlotDto>, Error>.Failure(
                Errors.ScheduleSlot.Database(ex.Message)
            );
        }
    }
}