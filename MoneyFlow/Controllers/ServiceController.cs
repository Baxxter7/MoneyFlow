using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyFlow.Managers;
using MoneyFlow.Models;

namespace MoneyFlow.Controllers;
[Authorize]
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

        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            // Visualízalos en consola o en una vista temporal
            Console.WriteLine(string.Join(", ", errors));

            
            return View(service);
        }

        //TODO: Cambiar el user id
      
        service.UserId = 1;
        var response = _manager.New(service);
        
        if(response == 1)
            return RedirectToAction(nameof(Index));
        
        ViewBag.message = "Error al crear servicio";
        return View();
    }
    
    [HttpGet]
    public IActionResult Edit(int id = 1)
    {
        //todo: Cambiar a dinamico
        var service = _manager.GetById(id);
        
        return View(service);
    }
    
    [HttpPost]
    public IActionResult Edit(ServiceVM viewModel )
    {
        //todo: Cambiar a dinamico
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            // Visualízalos en consola o en una vista temporal
            Console.WriteLine(string.Join(", ", errors));
            return View();
        }

        var response = _manager.Edit(viewModel);
        if(response == 1)
            return RedirectToAction(nameof(Index));
        
        ViewBag.message = "Error al crear servicio";
        return View();
    }
    
    public IActionResult Delete(int id )
    {
        //todo: Cambiar a dinamico
        var service = _manager.Delete(id);
        
        return RedirectToAction("Index");
    }
}