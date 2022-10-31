using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using UpPhoneBalance.API.Tests.Helpers;
using UpPhoneBalance.Domain.DataTransfers;
using UpPhoneBalance.Infrastructure.Data;
using UpPhoneBalance.Infrastructure.Models.Responses;
using Xunit;
using StatusCodes = UpPhoneBalance.Infrastructure.Enums.StatusCodes;

namespace UpPhoneBalance.API.Tests;

public class PaymentTests
{
    private HttpClient _httpClient;
    private PhonePaymentsDbContext _context;
    private const string Route = "/Payments/Add";

    ///<summary>
    /// Перед запуском сделать database update
    /// Для тестов используется отдельная база данных
    /// </summary>
    public PaymentTests()
    {
        var webHost = Utilites.SubstituteDbOnTestDb();
        _httpClient = webHost.CreateClient();
        _context = webHost.Services.CreateScope().ServiceProvider.GetService<PhonePaymentsDbContext>();
        _context.Prefixes.AddRange(Utilites.AddPrefixesToDb());
    }

    
    [Fact]
    public async Task AddPayment_WithCorrectData()
    {
        //Arrange
        var payment = Utilites.ReturnCorrectPaymentDto();
        
        //Act
        var response =
            await _httpClient.PostAndReturnResponseAsync<PaymentDto, PaymentResponse>(payment,
                Route);
        //Assert
        response.Should().NotBeNull();
        response.Value.Should().NotBeNull();
        response.ErrorModel.Should().BeNull();
        response.ValidationErrors.Should().BeNull();
    }

    [Fact]
    public async Task AddPayment_WithPhoneMask()
    {
        var payment = Utilites.ReturnCorrectPaymentDtoWithPhoneMask();
        
        //Act
        var response =
            await _httpClient.PostAndReturnResponseAsync<PaymentDto, PaymentResponse>(payment,
                Route);
        //Assert
        response.Should().NotBeNull();
        response.Value.Should().NotBeNull();
        response.ErrorModel.Should().BeNull();
        response.ValidationErrors.Should().BeNull();
    }

    [Fact]
    public async Task AddPayment_WithInvalidData()
    {
        //Arrange
        var payment = Utilites.ReturnInvalidPaymentDto();
        
        //Act
        var response =
            await _httpClient.PostAndReturnResponseAsync<PaymentDto, PaymentResponse>(payment, Route);
        
        //Assert
        response.Should().NotBeNull();
        response.Value.Should().BeNull();
        response.ValidationErrors.Should().NotBeNull();
    }

    [Fact]
    public async Task AddPayment_WithInvalidPrefix()
    {
        //Arrange
        var payment = Utilites.ReturnInvalidPrefixPaymentDto();
        
        //Act
        var response =
            await _httpClient.PostAndReturnResponseAsync<PaymentDto, PaymentResponse>(payment, Route);
        
        //Assert
        response.Should().NotBeNull();
        response.Value.Should().BeNull();
        response.ValidationErrors.Should().BeNull();
        response.ErrorModel.Should().NotBeNull();
        response.ErrorModel.StatusCode.Should().Be(StatusCodes.OperatorNotFound);
    }
}