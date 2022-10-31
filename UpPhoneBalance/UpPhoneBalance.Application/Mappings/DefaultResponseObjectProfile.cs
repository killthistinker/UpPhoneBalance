using Ardalis.Result;
using AutoMapper;
using UpPhoneBalance.Infrastructure.Models.Responses;

namespace UpPhoneBalance.Application.Mappings;

public class DefaultResponseObjectProfile : Profile
{
    public DefaultResponseObjectProfile()
    {
        CreateMap(typeof(Result<Task<DefaultResponseObject<PaymentResponse>>>), typeof(DefaultResponseObject<PaymentResponse>));
    }
}