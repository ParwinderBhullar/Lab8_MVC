using StudentWorkshopPortal.Repositories.Interfaces;
using StudentWorshopPortal.Models;
using System.Text.Json;

namespace StudentWorshopPortal.Data.Import;

public class JsonDataSeeder
{
    private readonly IWebHostEnvironment _environment;
    private readonly IWorkshopRepository _workshopRepository;
    private readonly IRegistrationRepository _registrationRepository;

    public JsonDataSeeder(
        IWebHostEnvironment environment,
        IWorkshopRepository workshopRepository,
        IRegistrationRepository registrationRepository)
    {
        _environment = environment;
        _workshopRepository = workshopRepository;
        _registrationRepository = registrationRepository;
    }

    public async Task SeedAsync()
    {
        var workshops = await _workshopRepository.GetAllAsync();

        if (workshops.Any())
            return;

        await SeedWorkshopsAsync();

        await SeedRegistrationsAsync();
    }

    private async Task SeedWorkshopsAsync()
    {
        string fileName = Path.Combine(
            _environment.ContentRootPath,
            "Data",
            "workshops.json");

        if (!File.Exists(fileName))
            return;

        string json = await File.ReadAllTextAsync(fileName);

        var workshops =
            JsonSerializer.Deserialize<List<JsonWorkshopDTO>>(json);

        if (workshops == null)
            return;

        foreach (var dto in workshops)
        {
            await _workshopRepository.AddAsync(new Workshop
            {
                Title = dto.Title,
                Instructor = dto.Instructor,
                Date = dto.Date,
                Location = dto.Location,
                Description = dto.Description
            });
        }

        await _workshopRepository.SaveChangesAsync();
    }

    private async Task SeedRegistrationsAsync()
    {
        string fileName = Path.Combine(
            _environment.ContentRootPath,
            "Data",
            "registrations.json");

        if (!File.Exists(fileName))
            return;

        string json = await File.ReadAllTextAsync(fileName);

        var registrations =
            JsonSerializer.Deserialize<List<JsonRegistrationDTO>>(json);

        if (registrations == null)
            return;

        var workshops = await _workshopRepository.GetAllAsync();

        foreach (var dto in registrations)
        {
            var workshop = workshops.FirstOrDefault(
                w => w.Id == dto.Id);

            if (workshop == null)
                continue;

            await _registrationRepository.AddAsync(new Registration
            {
                Id = dto.Id,
                StudentFullName = dto.StudentFullName,
                StudentNumber = dto.StudentNumber,
                EmailAddress = dto.EmailAddress,
                Comments = dto.Comments,
                WorkshopId = workshop.Id
            });
        }

        await _registrationRepository.SaveChangesAsync();
    }
}