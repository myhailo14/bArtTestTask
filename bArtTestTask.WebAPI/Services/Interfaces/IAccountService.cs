using bArtTestTask.Models;
using bArtTestTask.WebAPI.Models;

namespace bArtTestTask.WebAPI.Services.Interfaces;

public interface IAccountService
{
    Task<Account> GetAccountAsync(Guid id);
    IQueryable<Account> GetAllAccounts();
    Task AddAccountAsync(AccountCreationRequestDto request);
    Task UpdateAccountAsync(AccountDto accountDto);
    Task DeleteAccountAsync(Guid id);
}