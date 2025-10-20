using MoneyFlow.Context;
using MoneyFlow.DTOs;
using MoneyFlow.Entities;

namespace MoneyFlow.Managers;

public class TransactionManager
{
    private readonly AppDbContext _context;

    public TransactionManager(AppDbContext context)
    {
        _context = context;
    }

    public int New(TransactionDTO transaction)
    {
        Transaction entity = new Transaction
        {
            Date = transaction.Date,
            TotalAmount = transaction.TotalAmount,
            Comment = transaction.Comment,
            ServiceId = transaction.ServiceId,
            UserId = transaction.UserId
        };
        _context.Transaction.Add(entity);
        int affectedRows = _context.SaveChanges();
        return affectedRows;
    }

    public List<HistoryDTO> GetHistory(DateOnly startDate, DateOnly endDate, int UserId)
    {
        var listTransacctios = _context
            .Transaction
            .Where(item => item.UserId == UserId
                           && item.Date >= startDate
                           && item.Date <= endDate
            ).Select(item => new HistoryDTO
            {
                Date = item.Date.ToString("dd/MM/yyyy"),
                Month = item.Date.ToString("MMMM"),
                TypeService = item.Service.Type, //typeService
                Service = item.Service.Name,
                TotalAmount = item.TotalAmount.ToString()
            }).ToList();
        
        return listTransacctios;
    }
}