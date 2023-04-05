using System.Linq.Expressions;
using bArtTestTask.Models;

namespace bArtTestTask.WebAPI.Repositories.Interfaces;

public interface IIncidentRepository
{
    IQueryable<Incident> GetAll();
    IQueryable<Incident> GetAll(Expression<Func<Incident, bool>> expression);
    Task<Incident?> GetByNameOrDefaultAsync(string name);
    Task<Incident?> GetByNameOrDefaultAsync(string name, Incident? defaultValue);
    Task<Incident?> GetFirsOrDefaultAsync(Expression<Func<Incident, bool>> expression);
    Task<Incident?> GetFirsOrDefaultAsync(Expression<Func<Incident, bool>> expression, Incident? defaultValue);
    Task AddAsync(Incident incident);
    Task UpdateAsync(Incident incident);
    Task<bool> DeleteAsync(string name);
}