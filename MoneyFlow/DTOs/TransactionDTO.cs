namespace MoneyFlow.DTOs;

public class TransactionDTO
{
    public int ServiceId { get; set; }
    public int UserId { get; set; }
    public string Comment { get; set; }
    public DateOnly Date { get; set; }
    public decimal TotalAmount { get; set; }
}