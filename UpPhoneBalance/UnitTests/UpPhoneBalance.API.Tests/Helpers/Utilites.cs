using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UpPhoneBalance.Domain.DataTransfers;
using UpPhoneBalance.Domain.Entities;
using UpPhoneBalance.Infrastructure.Data;

namespace UpPhoneBalance.API.Tests.Helpers;

public static class Utilites
{
    private const string ConnectionString = "Server=localhost;Port=5432;Database=UpPhoneBalanceTestDb;UserId=postgres;Password=postgre;";
    public static WebApplicationFactory<Program> SubstituteDbOnTestDb()
    {
        return new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor =
                    services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<PhonePaymentsDbContext>));
                services.Remove(dbContextDescriptor);
                services.AddDbContext<PhonePaymentsDbContext>(opt =>
                {
                    opt.UseNpgsql(ConnectionString);
                });
            });
        });
    }

    public static PaymentDto ReturnCorrectPaymentDto() => new PaymentDto { Sum = 45, PhoneNumber = "87052345454" };
    public static PaymentDto ReturnCorrectPaymentDtoWithPhoneMask() => new PaymentDto { Sum = 45, PhoneNumber = "+7(705)234-54-54" };
    public static PaymentDto ReturnInvalidPaymentDto() => new PaymentDto { Sum = -100, PhoneNumber = "sdfdsfdsfsd" };
    public static PaymentDto ReturnInvalidPrefixPaymentDto() => new PaymentDto { Sum = 100, PhoneNumber = "+7(888)234-54-54" };

    public static List<PrefixesDbModel> AddPrefixesToDb()
    {
        return new List<PrefixesDbModel>
        {
            new PrefixesDbModel { Id = 1, Key = "707", MobileOperatorName = "Tele2" },
            new PrefixesDbModel { Id = 2, Key = "747", MobileOperatorName = "Tele2" },
            new PrefixesDbModel { Id = 3, Key = "705", MobileOperatorName = "Beeline" },
            new PrefixesDbModel { Id = 4, Key = "777", MobileOperatorName = "Beeline" },
            new PrefixesDbModel { Id = 5, Key = "701", MobileOperatorName = "Activ" },
            new PrefixesDbModel { Id = 6, Key = "700", MobileOperatorName = "Altel" },
            new PrefixesDbModel { Id = 7, Key = "708", MobileOperatorName = "Altel" }
        };
    }
}