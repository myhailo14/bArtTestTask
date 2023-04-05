using AutoMapper;
using bArtTestTask.Models;
using bArtTestTask.WebAPI.Exceptions;
using bArtTestTask.WebAPI.Helpers;
using bArtTestTask.WebAPI.Models;
using bArtTestTask.WebAPI.Repositories.Interfaces;
using bArtTestTask.WebAPI.Services.Interfaces;

namespace bArtTestTask.WebAPI.Services.Implementations;

public class ContactService : IContactService
{
    private readonly IContactRepository _repository;
    private readonly IMapper _mapper;

    public ContactService(IContactRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Contact> GetContactAsync(Guid id)
    {
        var contact = await _repository.GetByIdOrDefaultAsync(id);
        ExceptionHelper.ThrowRecordNotFoundIfNull(contact, id, nameof(Contact));

        return contact;
    }

    public IQueryable<Contact> GetAllContacts()
    {
        return _repository.GetAll();
    }

    public async Task AddContactAsync(ContactDto contactDto)
    {
        var contact = _mapper.Map<Contact>(contactDto);
        var contactFromDb = await _repository.GetFirsOrDefaultAsync(c => c.Email == contact.Email);

        ExceptionHelper.ThrowRecordAlreadyExistsIfNotNull(contactFromDb,
            $"{nameof(Contact)} with email {contact.Email} already exists.");

        contact.Account = null;
        await _repository.AddAsync(contact);
    }

    public async Task UpdateContactAsync(ContactDto contactDto)
    {
        var contact = _mapper.Map<Contact>(contactDto);

        var contactFromDb = await _repository.GetFirsOrDefaultAsync(c => c.Email == contact.Email);
        ExceptionHelper.ThrowRecordNotFoundIfNull(contactFromDb,
            $"{nameof(Contact)} with email {contact.Email} was not found.");

        contact.Id = contactFromDb.Id;
        contact.Account = contactFromDb.Account;
        await _repository.UpdateAsync(contact);
    }

    public async Task DeleteContactAsync(Guid id)
    {
        var deleted = await _repository.DeleteAsync(id);
        if (!deleted)
        {
            throw new RecordNotFoundException(id, nameof(Contact));
        }
    }
}