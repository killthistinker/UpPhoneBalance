using Microsoft.EntityFrameworkCore;
using UpPhoneBalance.Domain.Entities;

namespace UpPhoneBalance.Infrastructure.Data;

public class PhonePaymentsDbContext : DbContext
{
    public DbSet<PhonePaymentsDbModel> Payments { get; set; }
    public DbSet<PrefixesDbModel> Prefixes { get; set; }

    public PhonePaymentsDbContext(DbContextOptions<PhonePaymentsDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PrefixesDbModel>().HasData(
            new PrefixesDbModel[]
            {
                new PrefixesDbModel { Id = 1, Key = "701", MobileOperatorName = "Activ" },
                new PrefixesDbModel { Id = 2, Key = "777", MobileOperatorName = "Beeline" },
                new PrefixesDbModel { Id = 3, Key = "705", MobileOperatorName = "Beeline" },
                new PrefixesDbModel { Id = 4, Key = "707", MobileOperatorName = "Tele2" },
                new PrefixesDbModel { Id = 5, Key = "747", MobileOperatorName = "Tele2" },
                new PrefixesDbModel { Id = 6, Key = "700", MobileOperatorName = "Altel" },
                new PrefixesDbModel { Id = 7, Key = "708", MobileOperatorName = "Altel" }
            });
    }
}