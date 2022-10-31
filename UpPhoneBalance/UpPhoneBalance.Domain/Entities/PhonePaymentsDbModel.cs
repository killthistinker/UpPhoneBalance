namespace UpPhoneBalance.Domain.Entities;

public class PhonePaymentsDbModel
{
    public int Id { get; set; }
    public string OperatorName { get; set; }
    public string PhoneNumber { get; set; }
    public decimal Sum { get; set; }
    public string PaymentDate { get; set; }
}