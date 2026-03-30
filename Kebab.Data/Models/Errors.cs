namespace Kebab.Data.Models;

public enum ErrorType { NotFound, Validation, Unauthorized, RequestFailed }

public record Error(string Id, ErrorType Type, string Description);

public static class Errors
{
    public static Error AllTransmissionsFailed { get; } = new("AllTransmissionsFailed", ErrorType.RequestFailed, "All transmissions failed.");
    public static Error InsufficientFunds { get; } = new("InsufficientFunds", ErrorType.Validation, "Insufficient balance.");
}