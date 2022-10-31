using System.Globalization;
using AutoMapper;
using UpPhoneBalance.Domain.DataTransfers;
using UpPhoneBalance.Domain.Entities;

namespace UpPhoneBalance.Application.Mappings;

public class PaymentMapper : Profile
{
    public PaymentMapper()
    {
        CreateMap<PaymentDto, PhonePaymentsDbModel>()
            .ForMember(dst => dst.Id, opt => opt.Ignore())
            .ForMember(dst => dst.Sum, opt => opt.MapFrom(src => src.Sum))
            .ForMember(dst => dst.PaymentDate,
                opt => opt.MapFrom(src => DateTime.Now.ToString(CultureInfo.InvariantCulture)))
            .ForMember(dst => dst.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
    }
}