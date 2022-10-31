using UpPhoneBalance.Infrastructure.Enums;

namespace UpPhoneBalance.Infrastructure.Models;

public class ErrorModel
{
    public string Message { get; set; }
    public StatusCodes StatusCode { get; set; }
}