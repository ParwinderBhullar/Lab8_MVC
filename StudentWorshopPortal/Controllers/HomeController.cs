using Microsoft.AspNetCore.Mvc;
using StudentWorshopPortal.Services;

namespace StudentWorkshopPortal.Controllers;

public class HomeController : Controller
{
    private readonly WorkshopService _workshopService;

    public HomeController(WorkshopService workshopService)
    {
        _workshopService = workshopService;
    }

    [HttpGet("/")]
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Home";

        var workshops = await _workshopService.GetWorkshopsAsync();

        return View(workshops);
    }

    [HttpGet("/maintenance")]
    public IActionResult Maintenance()
    {
        ViewData["Title"] = "Maintenance";

        return View();
    }

    [HttpGet("/error")]
    public IActionResult Error()
    {
        ViewData["Title"] = "Error";

        return View();
    }
}