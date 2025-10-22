using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyFlow.Managers;
using MoneyFlow.Models;

namespace MoneyFlow.Controllers;

[Authorize]
public class ServiceController: BaseController
{
    private readonly ServiceManager _manager;
    public ServiceController(ServiceManager manager)
    {
        _manager = manager;
    }
    public IActionResult Index()
    {
       
        var serviceList =  _manager.GetAll(UserLoggedId);
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
            return View(service);
        }
        
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        service.UserId = UserLoggedId;
        var response = _manager.New(service);
        
        if(response == 1)
            return RedirectToAction(nameof(Index));
        
        ViewBag.message = "Error al crear servicio";
        return View();
    }
    
    [HttpGet]
    public IActionResult Edit(int id)
    {
   
        var service = _manager.GetById(UserLoggedId);
        
        return View(service);
    }
    
    [HttpPost]
    public IActionResult Edit(ServiceVM viewModel )
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            
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
        var service = _manager.Delete(id);
        
        return RedirectToAction("Index");
    }
}