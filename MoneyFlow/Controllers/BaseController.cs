using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MoneyFlow.Controllers;

[Authorize]
public abstract class BaseController : Controller
{
    protected int UserLoggedId
    {
        get
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return string.IsNullOrEmpty(id) ? 0 : int.Parse(id);
        }
    }
}