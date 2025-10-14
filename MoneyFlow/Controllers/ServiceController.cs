using Microsoft.AspNetCore.Mvc;
using MoneyFlow.Context;

namespace MoneyFlow.Controllers;

public class ServiceController : Controller
{
    private readonly AppDbContext _context;

    public ServiceController(AppDbContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        var serviceList = _context.Service.ToList();
        return View(serviceList);
    }
}