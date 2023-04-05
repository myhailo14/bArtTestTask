using System.Linq.Expressions;
using bArtTestTask.Models;
using bArtTestTask.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bArtTestTask.WebAPI.Repositories.Implementations;

public class AccountRepository : IAccountRepository
{
    private readonly DbContext _context;
    private readonly DbSet<Account> _accounts;

    public AccountRepository(DbContext context)
    {
        _context = context;
        _accounts = _context.Set<Account>();
    }

    public IQueryable<Account> GetAll()
    {
        return _accounts.AsQueryable();
    }

    public IQueryable<Account> GetAll(Expression<Func<Account, bool>> expression)
    {
        return _accounts.Where(expression);
    }

    public async Task<Account?> GetByIdOrDefaultAsync(Guid id)
    {
        return await _accounts.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Account?> GetByIdOrDefaultAsync(Guid id, Account? defaultValue)
    {
        return await _accounts.FirstOrDefaultAsync(a => a.Id == id) ?? defaultValue;
    }

    public async Task<Account?> GetFirsOrDefaultAsync(Expression<Func<Account, bool>> expression)
    {
        return await _accounts.FirstOrDefaultAsync(expression);
    }

    public async Task<Account?> GetFirsOrDefaultAsync(Expression<Func<Account, bool>> expression, Account? defaultValue)
    {
        return await _accounts.FirstOrDefaultAsync(expression) ?? defaultValue;
    }

    public async Task AddAsync(Account account)
    {
        await _context.AddAsync(account);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Account account)
    {
        _context.Update(account);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var account = await GetByIdOrDefaultAsync(id);
        if (account == null)
        {
            return false;
        }

        _context.Remove(account);
        await _context.SaveChangesAsync();

        return true;
    }
}