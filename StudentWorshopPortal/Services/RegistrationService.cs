using StudentWorkshopPortal.Repositories.Interfaces;
using StudentWorshopPortal.Models;

namespace StudentWorshopPortal.Services;

public class RegistrationService
{
    private readonly IRegistrationRepository _registrationRepository;
    private readonly IWorkshopRepository _workshopRepository;

    public RegistrationService(
        IRegistrationRepository registrationRepository,
        IWorkshopRepository workshopRepository)
    {
        _registrationRepository = registrationRepository;
        _workshopRepository = workshopRepository;
    }

    public async Task<IEnumerable<Registration>> GetRegistrationsAsync()
    {
        return await _registrationRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Registration>> GetRegistrationsByWorkshopAsync(int workshopId)
    {
        return await _registrationRepository.GetByWorkshopAsync(workshopId);
    }

    public async Task<Registration?> GetRegistrationAsync(int id)
    {
        return await _registrationRepository.GetByIdAsync(id);
    }

    public async Task SaveRegistrationAsync(Registration registration)
    {
        ArgumentNullException.ThrowIfNull(registration);

        var workshop = await _workshopRepository.GetByIdAsync(registration.Workshop.Id);

        if (workshop == null)
        {
            throw new InvalidOperationException(
                "Workshop does not exist.");
        }

        bool alreadyRegistered =
            await _registrationRepository.ExistsAsync(
                registration.StudentNumber,
                registration.Workshop.Id);

        if (alreadyRegistered)
        {
            throw new InvalidOperationException(
                "The student is already registered for this workshop.");
        }

        await _registrationRepository.AddAsync(registration);

        await _registrationRepository.SaveChangesAsync();
    }

    public async Task WithdrawRegistrationAsync(int id)
    {
        if (!await _registrationRepository.ExistsAsync(id))
        {
            throw new InvalidOperationException(
                $"Registration {id} was not found.");
        }

        await _registrationRepository.DeleteAsync(id);

        await _registrationRepository.SaveChangesAsync();
    }
}