namespace PolyclinicRegistryOffice.ErrorContext;

public static class Errors
{
    public static class Specialization
    {
        public static Error NotFound(int id)
        {
            return Error.NotFound(
                "specialization.not.found",
                $"Specialization with ID {id} not found."
            );
        }

        public static Error Database(string message)
        {
            return Error.Failure(
                "news.database.error",
                message
            );
        }

        public static Error InvalidCount(int count)
        {
            return Error.Validation(
                "news.invalid.count",
                $"Count must be a positive number. Provided: {count}"
            );
        }
    }

    public static class Specialist
    {
        public static Error NotFound(int id)
        {
            return Error.NotFound(
                "specialist.not.found",
                $"Specialist with ID {id} not found."
            );
        }
        
        public static Error Database(string message)
        {
            return Error.Failure(
                "specialist.database.error",
                message
            );
        }
    }

    public static class Appointment
    {
        public static Error NotFound(int id)
        {
            return Error.NotFound(
                "appointment.not.found",
                $"Appointment with ID {id} not found."
            );
        }
        public static Error Conflict()
        {
            return Error.Conflict(
                "appointment.time.conflict",
                "The selected appointment time is already booked or conflicts with the specialist's schedule."
            );
        }
    }

    public static class ScheduleSlot
    {
        public static Error NotFound(int id)
        {
            return Error.NotFound(
                "schedule.slot.not.found",
                $"Schedule slot with ID {id} not found."
            );
        }
        
        public static Error BulkEmpty()
        {
            return Error.Validation(
                "schedule.bulk.empty",
                "The list of schedule slots cannot be empty."
            );
        }
        
        public static Error Conflict()
        {
            return Error.Conflict(
                "schedule.slot.overlap",
                "The requested schedule slot overlaps with an existing slot for this specialist on this day."
            );
        }
        
        public static Error Conflict(string message)
        {
            return Error.Conflict(
                "schedule.slot.overlap",
                message
            );
        }
        
        public static Error Database(string message)
        {
            return Error.Failure(
                "schedule.slot.database.error",
                message
            );
        }
    }
    
    public static class Patient
    {
        public static Error NotFound(int id)
        {
            return Error.NotFound(
                "patient.not.found",
                $"Patient with ID {id} not found."
            );
        }

        public static Error AlreadyExists(string phoneNumber)
        {
            return Error.Conflict(
                "patient.already.exists",
                $"A patient with phone number {phoneNumber} is already registered."
            );
        }
    }
}