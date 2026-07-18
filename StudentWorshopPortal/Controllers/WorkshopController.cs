using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentWorshopPortal.Models;
using StudentWorshopPortal.Services;

namespace StudentWorkshopPortal.Controllers;

[Route("workshop")]
public class WorkshopController : Controller
{
    private readonly WorkshopService _workshopService;
    private readonly RegistrationService _registrationService;

    public WorkshopController(
        WorkshopService workshopService,
        RegistrationService registrationService)
    {
        _workshopService = workshopService;
        _registrationService = registrationService;
    }

    // Public: anyone can view the workshop list.
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var workshops = await _workshopService.GetWorkshopsAsync();

        return View(workshops);
    }

    // Public: anyone can view workshop details.
    [HttpGet("details/{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        var workshop = (await _workshopService
                .GetWorkshopsAsync())
            .FirstOrDefault(w => w.Id == id);

        if (workshop == null)
        {
            return NotFound();
        }

        return View(workshop);
    }

    // Protected: only authenticated students can open registration.
    [Authorize]
    [HttpGet("register")]
    public async Task<IActionResult> Register()
    {
        ViewBag.Workshops =
            await _workshopService.GetWorkshopsAsync();

        return View();
    }

    // Protected: only authenticated students can submit registration.
    [Authorize]
    [HttpPost("register")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(
        Registration registration)
    {
        var workshops =
            await _workshopService.GetWorkshopsAsync();

        ViewBag.Workshops = workshops;

        bool selectedWorkshopExists = workshops
            .Any(w => w.Title == registration.WorkshopTitle);

        if (!selectedWorkshopExists)
        {
            ModelState.AddModelError(
                nameof(registration.WorkshopTitle),
                "Please select an available workshop."
            );
        }

        if (!ModelState.IsValid)
        {
            return View(registration);
        }

        await _registrationService
            .SaveRegistrationAsync(registration);

        TempData["SuccessMessage"] =
            "Registration submitted successfully.";

        return RedirectToAction(nameof(Registrations));
    }

    // Protected: only authenticated students can view registrations.
    [Authorize]
    [HttpGet("registrations")]
    public async Task<IActionResult> Registrations()
    {
        var registrations =
            await _registrationService
                .GetRegistrationsAsync();

        return View(registrations);
    }

    // Protected: only authenticated students can withdraw.
    [Authorize]
    [HttpPost("withdraw/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Withdraw(int id)
    {
        await _registrationService
            .WithdrawRegistrationAsync(id);

        return RedirectToAction(nameof(Registrations));
    }

    // Public because the lab only requires registration-related
    // actions to be protected.
    [HttpGet("create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        Workshop workshop)
    {
        if (!ModelState.IsValid)
        {
            return View(workshop);
        }

        await _workshopService
            .SaveWorkshopAsync(workshop);

        TempData["SuccessMessage"] =
            "Workshop created successfully.";

        return RedirectToAction(nameof(Index));
    }
}