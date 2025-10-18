using Microsoft.AspNetCore.Mvc;
using MoneyFlow.DTOs;
using MoneyFlow.Managers;

namespace MoneyFlow.Controllers;

public class TransactionController : Controller
{
    private readonly ServiceManager _serviceManager;
    private readonly TransactionManager _transactionManager;
    
    public TransactionController(ServiceManager serviceManager, TransactionManager transactionManager)
    {
        _serviceManager = serviceManager;
        _transactionManager = transactionManager;
    }
    // GET
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ServicesByType(string typeService)
    {
        //TODO: CHANGE USER ID
        var userId = 1;
        
        var services= _serviceManager.GetByType(userId, typeService);
        return Ok(services);
    }

    [HttpPost]
    public IActionResult New([FromBody] TransactionDTO transactionDto)
    {
        //TODO: CHANGE USER ID
        transactionDto.UserId = 1;
        var response = _transactionManager.New(transactionDto);
        return Ok(response);
    }
    
}