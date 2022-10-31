using Ardalis.Result;

namespace UpPhoneBalance.Infrastructure.Models.Responses;

public class DefaultResponseObject<T>
{
    public T? Value { get; set; }
    public bool IsSuccess { get; set; }
    public ErrorModel ErrorModel { get; set; }
    public ValidationError[]? ValidationErrors { get; set; }
}