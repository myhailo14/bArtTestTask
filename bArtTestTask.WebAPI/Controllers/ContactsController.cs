using bArtTestTask.WebAPI.Exceptions;
using bArtTestTask.WebAPI.Helpers;
using bArtTestTask.WebAPI.Models;
using bArtTestTask.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace bArtTestTask.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactsController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet]
    public IActionResult GetContacts()
    {
        var contacts = _contactService.GetAllContacts();
        return Ok(contacts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetContact([FromQuery] Guid id)
    {
        try
        {
            var contact = await _contactService.GetContactAsync(id);
            return Ok(contact);
        }
        catch (RecordNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddContact([FromBody] ContactDto contactDto)
    {
        try
        {
            await _contactService.AddContactAsync(contactDto);
            return Accepted();
        }
        catch (RecordAlreadyExistsException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateContact([FromBody] ContactDto contactDto)
    {
        try
        {
            await _contactService.UpdateContactAsync(contactDto);
            return NoContent();
        }
        catch (RecordNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact([FromQuery] Guid id)
    {
        try
        {
            await _contactService.DeleteContactAsync(id);
            return NoContent();
        }
        catch (RecordNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}