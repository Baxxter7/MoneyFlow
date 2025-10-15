using Microsoft.AspNetCore.Mvc;
using MoneyFlow.Context;
using MoneyFlow.Managers;

namespace MoneyFlow.Controllers;

public class ServiceController : Controller
{
    private readonly ServiceManager _manager;

    public ServiceController(ServiceManager manager)
    {
        _manager = manager;
    }
    
    //Todo: Cambiar user ID
    public IActionResult Index()
    {
        var serviceList =  _manager.GetAll(1);
        return View(serviceList);
    }
}