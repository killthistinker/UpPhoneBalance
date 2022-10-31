using Ardalis.Result;
using UpPhoneBalance.Domain.DataTransfers;
using UpPhoneBalance.Infrastructure.Models.Responses;

namespace UpPhoneBalance.Infrastructure.Contracts.Services.Interfaces;

public interface IPaymentService
{
    public Task<Result<DefaultResponseObject<PaymentResponse>>> PaymentManager(PaymentDto model);
}