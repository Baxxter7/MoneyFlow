using Microsoft.AspNetCore.Mvc;
using MoneyFlow.Managers;
using MoneyFlow.Models;

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

    [HttpGet]
    public IActionResult New()
    {
        return View();
    }

    [HttpPost]
    public IActionResult New(ServiceVM service)
    {
        //TODO: Cambiar el user id
        service.UserId = 1;
        
        var response = _manager.New(service);
        
        if(response == 1)
            return RedirectToAction(nameof(Index));
        ViewBag.message = "Error al crear servicio";
        return View();
    }
}