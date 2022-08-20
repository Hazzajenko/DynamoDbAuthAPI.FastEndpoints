using Microsoft.AspNetCore.Mvc;

namespace DynamoDbAuthAPI.Controllers;

public class AuthController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}