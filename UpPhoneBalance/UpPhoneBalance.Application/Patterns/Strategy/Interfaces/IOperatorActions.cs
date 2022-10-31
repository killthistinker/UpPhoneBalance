using UpPhoneBalance.Domain.DataTransfers;

namespace UpPhoneBalance.Application.Patterns.Strategy.Interfaces;

public interface IOperatorActions
{ 
    public string SetPayment(string payment, PaymentDto model);
}