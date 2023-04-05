using System.Linq.Expressions;
using bArtTestTask.Models;
using bArtTestTask.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bArtTestTask.WebAPI.Repositories.Implementations;

public class ContactRepository : IContactRepository
{
    private readonly DbContext _context;
    private readonly DbSet<Contact> _contacts;

    public ContactRepository(DbContext context)
    {
        _context = context;
        _contacts = _context.Set<Contact>();
    }


    public IQueryable<Contact> GetAll()
    {
        return _contacts.AsQueryable();
    }

    public IQueryable<Contact> GetAll(Expression<Func<Contact, bool>> expression)
    {
        return _contacts.Where(expression);
    }

    public async Task<Contact?> GetByIdOrDefaultAsync(Guid id)
    {
        return await _contacts.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Contact?> GetByIdOrDefaultAsync(Guid id, Contact? defaultValue)
    {
        return await _contacts.FirstOrDefaultAsync(c => c.Id == id) ?? defaultValue;
    }

    public async Task<Contact?> GetFirsOrDefaultAsync(Expression<Func<Contact, bool>> expression)
    {
        return await _contacts.FirstOrDefaultAsync(expression);
    }

    public async Task<Contact?> GetFirsOrDefaultAsync(Expression<Func<Contact, bool>> expression, Contact? defaultValue)
    {
        return await _contacts.FirstOrDefaultAsync(expression) ?? defaultValue;
    }

    public async Task AddAsync(Contact contact)
    {
        await _context.AddAsync(contact);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Contact contact)
    {
        _context.Update(contact);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var contact = await GetByIdOrDefaultAsync(id);
        if (contact == null)
        {
            return false;
        }

        _context.Remove(contact);
        await _context.SaveChangesAsync();
        
        return true;
    }
}