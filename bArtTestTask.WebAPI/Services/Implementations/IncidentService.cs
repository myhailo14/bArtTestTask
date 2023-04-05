using AutoMapper;
using bArtTestTask.Models;
using bArtTestTask.WebAPI.Exceptions;
using bArtTestTask.WebAPI.Helpers;
using bArtTestTask.WebAPI.Models;
using bArtTestTask.WebAPI.Repositories.Implementations;
using bArtTestTask.WebAPI.Repositories.Interfaces;
using bArtTestTask.WebAPI.Services.Interfaces;
using Microsoft.VisualBasic;

namespace bArtTestTask.WebAPI.Services.Implementations;

public class IncidentService : IIncidentService
{
    private readonly IIncidentRepository _incidentRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IContactRepository _contactRepository;
    private readonly IMapper _mapper;

    public IncidentService(IIncidentRepository incidentRepository, IAccountRepository accountRepository,
        IContactRepository contactRepository, IMapper mapper)
    {
        _incidentRepository = incidentRepository;
        _accountRepository = accountRepository;
        _contactRepository = contactRepository;
        _mapper = mapper;
    }

    public async Task<Incident> GetIncidentAsync(string name)
    {
        var incident = await _incidentRepository.GetByNameOrDefaultAsync(name);
        ExceptionHelper.ThrowRecordNotFoundIfNull(incident, $"Incident with name {name} was not found.");
        return incident;
    }

    public IQueryable<Incident> GetAllIncidents()
    {
        return _incidentRepository.GetAll();
    }

    public async Task AddIncidentAsync(IncidentCreationRequestDto request)
    {
        var account = await _accountRepository.GetFirsOrDefaultAsync(a => a.Name == request.AccountName);
        ExceptionHelper.ThrowRecordNotFoundIfNull(account, $"Account with name {request.AccountName} was not found.");

        var contact = await _contactRepository.GetFirsOrDefaultAsync(c => c.Email == request.ContactEmail);
        if (contact != null)
        {
            contact.Account = account;
            contact.FirstName = request.ContactFirstName;
            contact.LastName = request.ContactLastName;

            await _contactRepository.UpdateAsync(contact);

            var newIncident = new Incident
            {
                Description = request.IncidentDescription,
                Accounts = new List<Account>()
            };
            await _incidentRepository.AddAsync(newIncident);

            account.Incident = newIncident;
            await _accountRepository.UpdateAsync(account);
            return;
        }

        contact = new Contact
        {
            Account = account,
            Email = request.ContactEmail,
            FirstName = request.ContactFirstName,
            LastName = request.ContactLastName
        };
        await _contactRepository.AddAsync(contact);

        var incident = new Incident
        {
            Description = request.IncidentDescription
        };
        await _incidentRepository.AddAsync(incident);

        account.Incident = incident;
        await _accountRepository.UpdateAsync(account);
    }

    public async Task UpdateIncidentAsync(IncidentDto incidentDto)
    {
        var incident = _mapper.Map<Incident>(incidentDto);
        var incidentFromDb = await _incidentRepository.GetByNameOrDefaultAsync(incident.Name);
        ExceptionHelper.ThrowRecordNotFoundIfNull(incidentFromDb,
            $"{nameof(Incident)} with name {incident.Name} was not found.");

        incident.Accounts = incidentFromDb.Accounts;

        await _incidentRepository.UpdateAsync(incident);
    }

    public async Task DeleteIncidentAsync(string name)
    {
        var deleted = await _incidentRepository.DeleteAsync(name);
        if (!deleted)
        {
            throw new RecordNotFoundException($"Incident with name {name} was not found.");
        }
    }
}