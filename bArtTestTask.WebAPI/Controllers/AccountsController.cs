using bArtTestTask.WebAPI.Exceptions;
using bArtTestTask.WebAPI.Models;
using bArtTestTask.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace bArtTestTask.WebAPI.Controllers;

[Controller]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public IActionResult GetAccounts()
    {
        var accounts = _accountService.GetAllAccounts();
        return Ok(accounts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccount([FromQuery] Guid id)
    {
        try
        {
            var account = await _accountService.GetAccountAsync(id);
            return Ok(account);
        }
        catch (RecordNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddAccount([FromBody] AccountCreationRequestDto request)
    {
        try
        {
            await _accountService.AddAccountAsync(request);
            return Accepted();
        }
        catch (RecordAlreadyExistsException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAccount([FromBody] AccountDto accountDto)
    {
        try
        {
            await _accountService.UpdateAccountAsync(accountDto);
            return NoContent();
        }
        catch (RecordNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAccount([FromQuery] Guid id)
    {
        try
        {
            await _accountService.DeleteAccountAsync(id);
            return NoContent();
        }
        catch (RecordNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}