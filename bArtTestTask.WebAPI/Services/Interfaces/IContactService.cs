using bArtTestTask.Models;
using bArtTestTask.WebAPI.Models;

namespace bArtTestTask.WebAPI.Services.Interfaces;

public interface IContactService
{
    Task<Contact> GetContactAsync(Guid id);
    IQueryable<Contact> GetAllContacts();
    Task AddContactAsync(ContactDto contactDto);
    Task UpdateContactAsync(ContactDto contactDto);
    Task DeleteContactAsync(Guid id);
}