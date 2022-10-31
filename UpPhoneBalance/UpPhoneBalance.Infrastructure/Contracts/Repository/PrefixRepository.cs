using Microsoft.EntityFrameworkCore;
using UpPhoneBalance.Domain.Entities;
using UpPhoneBalance.Infrastructure.Contracts.Repository.Interfaces;
using UpPhoneBalance.Infrastructure.Data;

namespace UpPhoneBalance.Infrastructure.Contracts.Repository;

public class PrefixRepository : IPrefixRepository
{
    private readonly PhonePaymentsDbContext _context;

    public PrefixRepository(PhonePaymentsDbContext context)
    {
        _context = context;
    }

    public async Task<PrefixesDbModel> GetPrefixByKeyAsync(string key) =>
        await _context.Prefixes.FirstOrDefaultAsync(p => p.Key == key);
 
    public IEnumerable<PrefixesDbModel>? GetAll() => _context.Prefixes;
    public void Create(PrefixesDbModel? item) => _context.Prefixes.Add(item);
    public void Update(PrefixesDbModel? item) => _context.Prefixes.Update(item);
    public void Remove(PrefixesDbModel? item) => _context.Prefixes.Remove(item);
    public void Save() => _context.SaveChanges();
    public async Task SaveAsync() => await _context.SaveChangesAsync();

}