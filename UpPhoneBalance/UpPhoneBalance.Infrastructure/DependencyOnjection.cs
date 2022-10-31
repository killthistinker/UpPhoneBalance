using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UpPhoneBalance.Infrastructure.Contracts.Repository;
using UpPhoneBalance.Infrastructure.Contracts.Repository.Interfaces;
using UpPhoneBalance.Infrastructure.Contracts.Services.Interfaces;
using UpPhoneBalance.Infrastructure.Data;

namespace UpPhoneBalance.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<PhonePaymentsDbContext>(options => options.UseNpgsql(connection));
        services.AddTransient<IPaymentRepository, PaymentRepository>();
        services.AddTransient<IPrefixRepository, PrefixRepository>();
        return services;
    }
}