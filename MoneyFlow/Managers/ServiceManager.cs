using Microsoft.EntityFrameworkCore;
using MoneyFlow.Context;
using MoneyFlow.Entities;
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

    public int New(ServiceVM viewModel)
    {
        var entity = new Service
        {
            Name = viewModel.Name,
            Type = viewModel.Type,
            UserId = viewModel.UserId
        };

        _context.Service.Add(entity);
        var rowsAftected = _context.SaveChanges();
        return rowsAftected;
    }

    public ServiceVM? GetById(int id)
    {
        var model = _context.Service
            .AsNoTracking()
            .Where(s => s.ServiceId == id)
            .Select(item => new ServiceVM
            {
                UserId = item.UserId,
                ServiceId = item.ServiceId,
                Name = item.Name,
                Type = item.Type
            })
            .FirstOrDefault();

        return model;
    }

    public int Edit(ServiceVM viewModel)
    {
        var entity = _context.Service.Find(viewModel.ServiceId);
        if (entity == null)
            return 0;

        entity.Name = viewModel.Name;
        entity.Type = viewModel.Type;

        _context.Service.Update(entity);
        var rowsAftected = _context.SaveChanges();
        return rowsAftected;
    }

    public int Delete(int id)
    {
        var entity = _context.Service.Find(id);
        _context.Service.Remove(entity);

        var rowsAftected = _context.SaveChanges();
        return rowsAftected;
    }
    public List<ServiceVM> GetByType(int userId, string type)
    {
        var listServicesByType = _context.Service
            .Where(item => item.Type == type && item.UserId == userId)
            .Select(item => new ServiceVM
            {
                ServiceId = item.ServiceId,
                Name = item.Name,
            }).ToList();
        
        return listServicesByType;
    }
}
