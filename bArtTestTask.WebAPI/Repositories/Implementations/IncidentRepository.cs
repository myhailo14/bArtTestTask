using System.Linq.Expressions;
using bArtTestTask.Models;
using bArtTestTask.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bArtTestTask.WebAPI.Repositories.Implementations;

public class IncidentRepository : IIncidentRepository
{
    private readonly DbContext _context;
    private readonly DbSet<Incident> _incidents;

    public IncidentRepository(DbContext context)
    {
        _context = context;
        _incidents = context.Set<Incident>();
    }
    
    public IQueryable<Incident> GetAll()
    {
        return _incidents.AsQueryable();
    }

    public IQueryable<Incident> GetAll(Expression<Func<Incident, bool>> expression)
    {
        return _incidents.Where(expression);
    }

    public async Task<Incident?> GetByNameOrDefaultAsync(string name)
    {
        return await _incidents.FirstOrDefaultAsync(i => i.Name == name);
    }

    public async Task<Incident?> GetByNameOrDefaultAsync(string name, Incident? defaultValue)
    {
        return await _incidents.FirstOrDefaultAsync(i => i.Name == name) ?? defaultValue;
    }

    public async Task<Incident?> GetFirsOrDefaultAsync(Expression<Func<Incident, bool>> expression)
    {
        return await _incidents.FirstOrDefaultAsync(expression);
    }

    public async Task<Incident?> GetFirsOrDefaultAsync(Expression<Func<Incident, bool>> expression, Incident? defaultValue)
    {
        return await _incidents.FirstOrDefaultAsync(expression) ?? defaultValue;
    }

    public async Task AddAsync(Incident incident)
    {
        await _context.AddAsync(incident);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Incident incident)
    {
        _context.Update(incident);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(string name)
    {
        var incident = await GetByNameOrDefaultAsync(name);
        if (incident == null)
        {
            return false;
        }

        _context.Remove(incident);
        await _context.SaveChangesAsync();

        return true;
    }
}