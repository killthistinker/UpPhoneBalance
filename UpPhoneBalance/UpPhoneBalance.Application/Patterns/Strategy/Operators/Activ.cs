using UpPhoneBalance.Application.Patterns.Strategy.Interfaces;
using UpPhoneBalance.Domain.DataTransfers;

namespace UpPhoneBalance.Application.Patterns.Strategy.Operators;

public class Activ : IOperatorActions
{
    public string SetPayment(string payment, PaymentDto model) => $"Платеж оператору {payment}, на сумму {model.Sum} успешно совершен";
}