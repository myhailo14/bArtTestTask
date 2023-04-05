using System.Linq.Expressions;
using bArtTestTask.Models;

namespace bArtTestTask.WebAPI.Repositories.Interfaces;

public interface IAccountRepository
{
    IQueryable<Account> GetAll();
    IQueryable<Account> GetAll(Expression<Func<Account, bool>> expression);
    Task<Account?> GetByIdOrDefaultAsync(Guid id);
    Task<Account?> GetByIdOrDefaultAsync(Guid id, Account? defaultValue);
    Task<Account?> GetFirsOrDefaultAsync(Expression<Func<Account, bool>> expression);
    Task<Account?> GetFirsOrDefaultAsync(Expression<Func<Account, bool>> expression, Account? defaultValue);
    Task AddAsync(Account account);
    Task UpdateAsync(Account account);
    Task<bool> DeleteAsync(Guid id);
}