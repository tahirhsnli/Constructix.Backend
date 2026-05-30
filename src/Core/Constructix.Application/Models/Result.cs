using Constructix.Domain.Abstractions;

namespace Constructix.Application.Models;

/// <summary>
/// Tahir, bu klass sənin layihənin bütün "Nəticə" idarəetməsini tək başına həll edir.
/// T: Uğurlu nəticənin tipi, TError: IDomainError interfeysini tətbiq edən xəta tipi.
/// </summary>
public class Result<T, TError> where TError : IDomainError
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public TError? Error { get; }

    // Constructor-u private saxlayırıq ki, ancaq Success və Failure metodları ilə yaradılsın
    private Result(T? value, bool isSuccess, TError? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    // Uğurlu nəticə üçün
    public static Result<T, TError> Success(T value) => new(value, true, default);

    // Xətalı nəticə üçün
    public static Result<T, TError> Failure(TError error) => new(default, false, error);

    // --- SENIOR TOXUNUŞU: Implicit Operatorlar ---

    // Əgər metod "return tokenString;" qaytarsa, avtomatik Success-ə bükülür
    public static implicit operator Result<T, TError>(T value) => Success(value);

    // Əgər metod "return DomainError.NotFound();" qaytarsa, avtomatik Failure-a bükülür
    public static implicit operator Result<T, TError>(TError error) => Failure(error);
}