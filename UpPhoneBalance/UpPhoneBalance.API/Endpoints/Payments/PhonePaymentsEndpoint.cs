using System.ComponentModel;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UpPhoneBalance.Domain.DataTransfers;
using UpPhoneBalance.Infrastructure.Contracts.Services.Interfaces;
using UpPhoneBalance.Infrastructure.Models.Responses;
using StatusCodes = UpPhoneBalance.Infrastructure.Enums.StatusCodes;

namespace UpPhoneBalance.API.Endpoints.Payments;

[ApiController]
[Route("[controller]")]
public class PhonePaymentsEndpoint : EndpointBaseAsync.WithRequest<PaymentDto>.WithActionResult<DefaultResponseObject<PaymentResponse>>
{
    private readonly IPaymentService _paymentService;
    private readonly IMapper _mapper;
    private readonly ILogger<PhonePaymentsEndpoint> _logger;

    public PhonePaymentsEndpoint(IPaymentService paymentService, IMapper mapper, ILogger<PhonePaymentsEndpoint> logger)
    {
        _paymentService = paymentService;
        _mapper = mapper;
        _logger = logger;
    }
    
    [HttpPost("/Payments/Add")]
    [SwaggerOperation(
        Summary = "Пополнение баланса номера телефона",
        Description = "Для оплаты необходимо ввести сумму и номер телефона в формате +7(000)000-00-00 либо начиная с 8",
        Tags = new[] { "Payments" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<PaymentResponse>>> HandleAsync(PaymentDto request, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            _logger.LogInformation($"Входные данные: Номер телефона: {request.PhoneNumber}, Сумма: {request.Sum}");
            var response = await _paymentService.PaymentManager(request);
            if (!response.IsSuccess) return Ok(response);
            return Ok(_mapper.Map<DefaultResponseObject<PaymentResponse>>(response));
        }
        catch (Exception exception)
        {
            _logger.LogCritical(exception,"Сработал cath");
            return Ok(StatusCodes.ServiceIsUnavailable);
        }
    }
}