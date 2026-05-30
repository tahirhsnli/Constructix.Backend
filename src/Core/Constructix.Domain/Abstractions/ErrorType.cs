namespace Constructix.Domain.Abstractions;
public abstract class ErrorType(string name, int value) : SmartEnum<ErrorType>(name, value)
{
    public static readonly ErrorType Conflict = new ConflictEnum();
    public static readonly ErrorType NotFound = new NotFoundEnum();
    public static readonly ErrorType BadRequest = new BadRequestEnum();
    public static readonly ErrorType Validation = new ValidationEnum();
    public static readonly ErrorType Unexpected = new UnexpectedEnum();
    public static readonly ErrorType Unauthorized = new UnexpectedEnum();
    public static readonly ErrorType InfrastructureError = new InfrastructureEnum();
    public static readonly ErrorType BusinessRuleViolationError = new BusinessRuleViolationEnum();

    private class ConflictEnum : ErrorType
    {
        public ConflictEnum() : base("Conflict", 0) { }
    }

    private class NotFoundEnum : ErrorType
    {
        public NotFoundEnum() : base("NotFound", 1) { }
    }

    private class BadRequestEnum : ErrorType
    {
        public BadRequestEnum() : base("BadRequest", 2) { }
    }

    private class ValidationEnum : ErrorType
    {
        public ValidationEnum() : base("Validation", 3) { }
    }
    private class UnexpectedEnum : ErrorType
    {
        public UnexpectedEnum() : base("Unexpected", 4) { }
    }
    private class UnauthorizedEnum : ErrorType
    {
        public UnauthorizedEnum() : base("Unauthorized", 5) { }
    }

    private class InfrastructureEnum : ErrorType
    {
        public InfrastructureEnum() : base("InfrastructureError", 6) { }
    }

    private class BusinessRuleViolationEnum : ErrorType
    {
        public BusinessRuleViolationEnum() : base("BusinessRuleViolationError", 7) { }
    }
}
