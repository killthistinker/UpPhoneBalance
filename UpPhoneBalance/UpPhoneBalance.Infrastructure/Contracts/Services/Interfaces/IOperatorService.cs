using UpPhoneBalance.Domain.DataTransfers;

namespace UpPhoneBalance.Infrastructure.Contracts.Services.Interfaces;

public interface IOperatorService
{
    public Task<string>? GetOperatorName(PaymentDto payment);
    public string SetPaymentToOperator(string operatorName, PaymentDto payment);
}