using Microsoft.EntityFrameworkCore;
using MoneyFlow.Context;
using MoneyFlow.Models;

namespace MoneyFlow.Managers;

public class ServiceManager
{
    private readonly AppDbContext _context;

    public ServiceManager(AppDbContext context)
    {
        _context = context;
    }

    public List<ServiceVM> GetAll(int userId)
    {
        var serviceList = _context.Service 
            .Where(s => s.UserId == userId)
            .AsNoTracking()
            .Select(item => new ServiceVM
            {
                UserId = item.UserId,
                ServiceId = item.ServiceId,
                Name = item.Name,
                Type = item.Type
            })
            .ToList();
        
        return serviceList;
    }
}
