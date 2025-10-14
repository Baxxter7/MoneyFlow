namespace MoneyFlow.Entities;

public class Service
{
    public int ServiceId { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    
    public User User { get; set; }
}