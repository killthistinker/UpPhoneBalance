using Microsoft.Extensions.Logging;
using UpPhoneBalance.Application.Patterns.Strategy.Interfaces;
using UpPhoneBalance.Application.Patterns.Strategy.Operators;
using UpPhoneBalance.Domain.DataTransfers;
using UpPhoneBalance.Infrastructure.Contracts.Repository.Interfaces;
using UpPhoneBalance.Infrastructure.Contracts.Services.Interfaces;

namespace UpPhoneBalance.Application.Services;

public class OperatorService : IOperatorService
{
    private readonly IPrefixRepository _repository;
    private IOperatorActions _setPayment;
    private readonly ILogger<OperatorService> _logger;
    private const int PrefixLength = 3;
    public OperatorService(IPrefixRepository repository, ILogger<OperatorService> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task<string>? GetOperatorName(PaymentDto model)
    {
        try
        {
            string prefix = GetPrefixKey(model);
            var prefixModel = await _repository.GetPrefixByKeyAsync(prefix);
            if (prefixModel is null) return null;
            var value = prefixModel.MobileOperatorName;
            _logger.LogInformation($"Успешно.Префикс: {prefix}.Оператор: {prefixModel.MobileOperatorName}");
            return value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Сработал catch");
            return null;
        }
    }

    ///<summary>
    ///В данном методе реализован паттерн стратегия,
    /// еслм в switch присутствует оператор, данные отправляются в заглушку,
    /// заглушка возвращает ответ
    /// </summary>
    public string SetPaymentToOperator(string operatorName, PaymentDto payment)
    {
        switch (operatorName)
        {
            case "Activ":
                _setPayment = new Activ();
                break;
            case "Beeline":
                _setPayment = new Beeline();
                break;
            case "Altel":
                _setPayment = new Altel();
                break;
            case "Tele2":
                _setPayment = new Tele2();
                break;
            default:
                return null;
        }

        string response = _setPayment.SetPayment(operatorName, payment);
        
        return response;
    }

    ///<summary>
    /// Метод обрезает строку номера телефона
    /// для получения префикса и хранения
    /// номера в базе данных в едином формате
    /// </summary>
    private static string GetPrefixKey(PaymentDto model)
    {
        if (model.PhoneNumber[0] == '8')
        {
            string key = model.PhoneNumber.TrimStart('8').Substring(0, PrefixLength);
            model.PhoneNumber = model.PhoneNumber.Trim('8');
            return key;
        }
        model.PhoneNumber = model.PhoneNumber
            .Replace("(", "")
            .Replace(")", "")
            .Replace("-", "")
            .Replace(" ", "")
            .Replace("+7", "");
        string prefix = model.PhoneNumber.Substring(0, PrefixLength);
        return prefix;
    }
}