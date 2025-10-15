using Microsoft.EntityFrameworkCore;
using MoneyFlow.Context;
using MoneyFlow.Entities;

namespace MoneyFlow.Managers;

public class ServiceManager
{
    private readonly AppDbContext _context;

    public ServiceManager(AppDbContext context)
    {
        _context = context;
    }

    public List<Service> GetAll(int userId)
    {
        var serviceList = _context.Service 
            .Where(s => s.UserId == userId)
            .AsNoTracking()
            .ToList();
        
        return serviceList;
    }
}
