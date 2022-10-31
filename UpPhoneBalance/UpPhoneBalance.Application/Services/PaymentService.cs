using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using UpPhoneBalance.Domain.DataTransfers;
using UpPhoneBalance.Domain.Entities;
using UpPhoneBalance.Infrastructure.Contracts.Repository.Interfaces;
using UpPhoneBalance.Infrastructure.Contracts.Services.Interfaces;
using UpPhoneBalance.Infrastructure.Enums;
using UpPhoneBalance.Infrastructure.Models;
using UpPhoneBalance.Infrastructure.Models.Responses;

namespace UpPhoneBalance.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<PaymentDto> _validator;
    private readonly IOperatorService _operatorService;
    private readonly ILogger<PaymentService> _logger;
    private readonly IStringLocalizer<PaymentService> _localizer;
    public PaymentService( IPaymentRepository paymentRepository, IMapper mapper, IValidator<PaymentDto> validator, IOperatorService prefixService, ILogger<PaymentService> logger, IStringLocalizer<PaymentService> localizer)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
        _validator = validator;
        _operatorService = prefixService;
        _logger = logger;
        _localizer = localizer;
    }

    ///<summary>
    ///Метод действует как распределитель данных,
    /// Если в базе найден оператор, отправяет данные в сервис распределения оператора,
    /// После чего записывает данные в базу и возвращает ответ 
    /// </summary>
    public async Task<Result<DefaultResponseObject<PaymentResponse>>> PaymentManager(PaymentDto model)
    {
        try
        {
            var validationResult = _validator.Validate(model);
            if (!validationResult.IsValid) return Result<DefaultResponseObject<PaymentResponse>>.Invalid(validationResult.AsErrors());
     
            var operatorName = await _operatorService.GetOperatorName(model)!;
            if (operatorName is null)
            {
                _logger.LogInformation($"Оператор не найден. {model.PhoneNumber}");
                return new DefaultResponseObject<PaymentResponse>
                {
                    ErrorModel = new ErrorModel{StatusCode = StatusCodes.OperatorNotFound, Message = _localizer["OperatorNotSupported"]}
                };
            }
            
            string response = _operatorService.SetPaymentToOperator(operatorName, model);
            bool result = await AddPayment(model, operatorName,response);
            if (!result)
            {
                _logger.LogInformation($"Ошибка сервиса. Ответ оператора: ${response}");
                return new DefaultResponseObject<PaymentResponse>
                {
                    ErrorModel = new ErrorModel{StatusCode = StatusCodes.ServiceIsUnavailable, Message = _localizer["ServiceUnavailable"]}
                };
            }
            _logger.LogInformation($"Сервис отработал. Ответ оператора: {response}");
            return new DefaultResponseObject<PaymentResponse>{Value = new PaymentResponse{Message = response}};
        }
        catch (Exception exception)
        {
            _logger.LogCritical(exception, "Сработал cath");
            return new DefaultResponseObject<PaymentResponse>
            {
                ErrorModel = new ErrorModel{StatusCode = StatusCodes.ServiceIsUnavailable, Message = _localizer["ServiceUnavailable"]}
            };
        }
    }

    private async Task<bool> AddPayment(PaymentDto model, string operatorName, string response)
    {
        if (response is null) return false;
        
        var payment = _mapper.Map<PhonePaymentsDbModel>(model);
        if (payment is null) return false;
        
        payment.OperatorName = operatorName;
        _paymentRepository.Create(payment); 
        await _paymentRepository.SaveAsync();
        return true;
    }
}