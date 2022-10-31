using System.Reflection;
using System.Globalization;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using UpPhoneBalance.Application.Mappings;
using UpPhoneBalance.Application.Patterns.Strategy.Interfaces;
using UpPhoneBalance.Application.Services;
using UpPhoneBalance.Infrastructure.Contracts.Services.Interfaces;

namespace UpPhoneBalance.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IPaymentService, PaymentService>();
        serviceCollection.AddTransient<IOperatorService, OperatorService>();
        serviceCollection.AddAutoMapper(typeof(PaymentMapper), typeof(DefaultResponseObjectProfile));
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        serviceCollection.AddLocalization(options => options.ResourcesPath = "Resources");
        return serviceCollection;
    }
}