using AutoMapper;
using bArtTestTask.Models;
using bArtTestTask.WebAPI.Exceptions;
using bArtTestTask.WebAPI.Helpers;
using bArtTestTask.WebAPI.Models;
using bArtTestTask.WebAPI.Repositories.Interfaces;
using bArtTestTask.WebAPI.Services.Interfaces;
using Microsoft.VisualBasic;

namespace bArtTestTask.WebAPI.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IContactRepository _contactRepository;
    private readonly IMapper _mapper;

    public AccountService(IAccountRepository accountRepository, IMapper mapper, IContactRepository contactRepository)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
        _contactRepository = contactRepository;
    }


    public async Task<Account> GetAccountAsync(Guid id)
    {
        var account = await _accountRepository.GetByIdOrDefaultAsync(id);
        ExceptionHelper.ThrowRecordNotFoundIfNull(account, id, nameof(Account));

        return account;
    }

    public IQueryable<Account> GetAllAccounts()
    {
        return _accountRepository.GetAll();
    }

    public async Task AddAccountAsync(AccountCreationRequestDto request)
    {
        var contact = await _contactRepository.GetByIdOrDefaultAsync(request.ContactId);
        ExceptionHelper.ThrowRecordNotFoundIfNull(contact, request.ContactId, nameof(Contact));

        var accountFromDb = await _accountRepository.GetFirsOrDefaultAsync(a => a.Name == request.Name);
        ExceptionHelper.ThrowRecordAlreadyExistsIfNotNull(accountFromDb,
            $"{nameof(Account)} with name {request.Name} already exists.");

        var newAccount = new Account
        {
            Name = request.Name,
            Contacts = new List<Contact> {contact}
        };
        await _accountRepository.AddAsync(newAccount);
    }

    public async Task UpdateAccountAsync(AccountDto accountDto)
    {
        var account = _mapper.Map<Account>(accountDto);
        var accountFromDb = await _accountRepository.GetFirsOrDefaultAsync(a => a.Name == account.Name);
        ExceptionHelper.ThrowRecordNotFoundIfNull(accountFromDb,
            $"{nameof(Account)} with name {account.Name} was not found.");

        account.Contacts = accountFromDb.Contacts;
        account.Id = accountFromDb.Id;
        account.Incident = accountFromDb.Incident;

        await _accountRepository.UpdateAsync(account);
    }

    public async Task DeleteAccountAsync(Guid id)
    {
        var deleted = await _accountRepository.DeleteAsync(id);
        if (!deleted)
        {
            throw new RecordNotFoundException(id, nameof(Account));
        }
    }
}