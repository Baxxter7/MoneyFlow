using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyFlow.DTOs;
using MoneyFlow.Managers;

namespace MoneyFlow.Controllers;
[Authorize]
public class TransactionController : BaseController
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
        var userId = UserLoggedId;
        
        var services= _serviceManager.GetByType(userId, typeService);
        return Ok(services);
    }

    [HttpPost]
    public IActionResult New([FromBody] TransactionDTO transactionDto)
    {

        transactionDto.UserId = UserLoggedId;
        var response = _transactionManager.New(transactionDto);
        return Ok(response);
    }
    
    public IActionResult History()
    {
        return View();
    }

    [HttpGet]
    public IActionResult HistoryTransaction(DateOnly startDate, DateOnly endDate, int UserId)
    {
        UserId = UserLoggedId;
        var listTransactions = _transactionManager.GetHistory(startDate, endDate, UserId);
        return Ok(new {  
            data = listTransactions
        });
    }
    
}