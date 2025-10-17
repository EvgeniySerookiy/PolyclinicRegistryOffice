using PolyclinicRegistryOffice.Entities;

namespace PolyclinicRegistryOffice.ErrorContext;

public class Result<T, E>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public E? Error { get; }

    private Result(bool isSuccess, T? value, E? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static Result<T, E> Success(T value) =>
        new(true, value, default);
    
    public static Result<T, E> Failure(E error) =>
        new(false, default, error);
}