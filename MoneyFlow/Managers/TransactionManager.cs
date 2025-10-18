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
}