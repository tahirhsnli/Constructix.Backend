namespace Constructix.Domain.Abstractions;
public record DomainError : IDomainError
{
    public static DomainError Conflict(string? messageKey = "ConflictDataProvided") =>
        new(messageKey, ErrorType.Conflict);

    public static DomainError NotFound(string? messageKey = "ItemNotFound") =>
        new(messageKey, ErrorType.NotFound);

    public static DomainError BadRequest(string? messageKey = "InvalidRequest") =>
        new(messageKey, ErrorType.BadRequest);

    public static DomainError Validation(string? messageKey = "ValidationFailed", List<string>? errors = null) =>
        new(messageKey, ErrorType.Validation, errors);

    public static DomainError UnExpected(string? messageKey = "UnexpectedError") =>
        new(messageKey, ErrorType.Unexpected);

    public static DomainError Unauthorized(string? messageKey = "UnauthorizedError") =>
      new(messageKey, ErrorType.Unauthorized);

    public static DomainError InfrastructureError(string? messageKey = "InfrastructureError") =>
    new(messageKey, ErrorType.InfrastructureError);

    public static DomainError BusinessRuleViolation(string? messageKey = "BusinessRuleViolation") =>
        new(messageKey, ErrorType.BusinessRuleViolationError);
    private DomainError(string? messageKey, ErrorType errorType, List<string>? errors = null)
    {
        ErrorMessage = messageKey;
        ErrorType = errorType;
        Errors = errors ?? new List<string>();
    }

    public string? ErrorMessage { get; init; }
    public ErrorType ErrorType { get; init; }
    public List<string>? Errors { get; init; }
}