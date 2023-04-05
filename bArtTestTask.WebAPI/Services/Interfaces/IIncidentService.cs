using bArtTestTask.Models;
using bArtTestTask.WebAPI.Models;

namespace bArtTestTask.WebAPI.Services.Interfaces;

public interface IIncidentService
{
    Task<Incident> GetIncidentAsync(string name);
    IQueryable<Incident> GetAllIncidents();
    Task AddIncidentAsync(IncidentCreationRequestDto request);
    Task UpdateIncidentAsync(IncidentDto incidentDto);
    Task DeleteIncidentAsync(string name);
}