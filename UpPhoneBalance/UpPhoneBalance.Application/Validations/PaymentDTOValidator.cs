using FluentValidation;
using Microsoft.Extensions.Localization;
using UpPhoneBalance.Domain.DataTransfers;

namespace UpPhoneBalance.Application.Validations;

public class PaymentDtoValidator : AbstractValidator<PaymentDto>
{
    public PaymentDtoValidator(IStringLocalizer<PaymentDtoValidator> localizer)
    {
        RuleFor(p => p.Sum).GreaterThan(0).WithMessage(localizer["InvalidValue"]).NotNull()
            .WithMessage(localizer["RequiredField"]);
        RuleFor(p => p.PhoneNumber).NotNull().WithMessage(localizer["RequiredField"]).MaximumLength(16).WithMessage(localizer["InvalidFormat"]).MinimumLength(10)
            .WithMessage(localizer["InvalidFormat"]);
    }
}