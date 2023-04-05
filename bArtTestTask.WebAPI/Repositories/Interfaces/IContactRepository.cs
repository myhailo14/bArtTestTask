using System.Linq.Expressions;
using bArtTestTask.Models;

namespace bArtTestTask.WebAPI.Repositories.Interfaces;

public interface IContactRepository
{
    IQueryable<Contact> GetAll();
    IQueryable<Contact> GetAll(Expression<Func<Contact, bool>> expression);
    Task<Contact?> GetByIdOrDefaultAsync(Guid id);
    Task<Contact?> GetByIdOrDefaultAsync(Guid id, Contact? defaultValue);
    Task<Contact?> GetFirsOrDefaultAsync(Expression<Func<Contact, bool>> expression);
    Task<Contact?> GetFirsOrDefaultAsync(Expression<Func<Contact, bool>> expression, Contact? defaultValue);
    Task AddAsync(Contact contact);
    Task UpdateAsync(Contact contact);
    Task<bool> DeleteAsync(Guid id);
}