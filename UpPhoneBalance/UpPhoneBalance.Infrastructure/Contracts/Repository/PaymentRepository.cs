using UpPhoneBalance.Domain.Entities;
using UpPhoneBalance.Infrastructure.Contracts.Repository.Interfaces;
using UpPhoneBalance.Infrastructure.Data;

namespace UpPhoneBalance.Infrastructure.Contracts.Repository;

public class PaymentRepository : IPaymentRepository
{
    private readonly PhonePaymentsDbContext _context;

    public PaymentRepository(PhonePaymentsDbContext context)
    {
        _context = context;
    }

    public IEnumerable<PhonePaymentsDbModel>? GetAll() => _context.Payments;
    public void Create(PhonePaymentsDbModel item) => _context.Payments.Add(item);
    public void Update(PhonePaymentsDbModel item) => _context.Payments.Update(item);
    public void Remove(PhonePaymentsDbModel item) => _context.Payments.Remove(item);
    public void Save() => _context.SaveChanges();
    public async Task SaveAsync() => await _context.SaveChangesAsync();
}