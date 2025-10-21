using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoneyFlow.Managers;
using MoneyFlow.Models;

namespace MoneyFlow.Controllers;

public class AccountController : Controller
{
    private readonly UserManager _userManager;

    public AccountController(UserManager userManager)
    {
        _userManager = userManager;
    }

    // GET
    public IActionResult Login()
    {
        var viewModel = new LoginVM();
        return View(viewModel);
    }
    
    [HttpPost]
    public IActionResult Login(LoginVM login)
    {
        if(!ModelState.IsValid) return View(login);

        var found = _userManager.Login(login);
        if (found is null )
        {
            ViewBag.Message = "Invalid username or password";
            return View(login); 
        }else {
            return RedirectToAction("Index", "Home");
        }
    }

    public IActionResult Register()
    {
        var userView = new UserVM();
        return View(userView);
    }
    
    [HttpPost]
    public IActionResult Register(UserVM user)
    {
        if(!ModelState.IsValid) return View(user);

        try
        {
            var response = _userManager.Register(user);
            if (response != 0)
            {
                ViewBag.message = "Your account has been registered, please try logging in.";
                ViewBag.Class = "alert-success";
            }
            else
            {
                ViewBag.message = "Your account could not be registered.";
                ViewBag.Class = "alert-danger";
            }
        }
        catch (Exception e)
        {
            ViewBag.message = e.Message; 
            ViewBag.Class = "alert-danger";
        }
        
        return View(user);
    }
}