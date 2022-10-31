namespace UpPhoneBalance.Domain.DataTransfers;

public class PaymentDto
{
    public string PhoneNumber { get; set; }
    public decimal Sum { get; set; }
}