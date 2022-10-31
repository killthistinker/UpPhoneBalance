using UpPhoneBalance.Domain.Entities;

namespace UpPhoneBalance.Infrastructure.Contracts.Repository.Interfaces;

public interface IPrefixRepository : IRepository<PrefixesDbModel>
{
    public Task<PrefixesDbModel>  GetPrefixByKeyAsync(string key);
}