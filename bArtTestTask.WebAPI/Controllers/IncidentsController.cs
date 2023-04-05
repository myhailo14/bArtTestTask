using bArtTestTask.Models;
using bArtTestTask.WebAPI.Exceptions;
using bArtTestTask.WebAPI.Helpers;
using bArtTestTask.WebAPI.Models;
using bArtTestTask.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace bArtTestTask.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class IncidentsController : ControllerBase
{
    private readonly IIncidentService _incidentService;

    public IncidentsController(IIncidentService incidentService)
    {
        _incidentService = incidentService;
    }

    [HttpGet]
    public IActionResult GetIncidents()
    {
        var incidents = _incidentService.GetAllIncidents();
        return Ok(incidents);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetIncident([FromQuery] string name)
    {
        try
        {
            var incident = await _incidentService.GetIncidentAsync(name);
            return Ok(incident);
        }
        catch (RecordNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddIncident([FromBody] IncidentCreationRequestDto requestDto)
    {
        try
        {
            await _incidentService.AddIncidentAsync(requestDto);
            return Accepted();
        }
        catch (RecordNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateIncident([FromBody] IncidentDto incidentDto)
    {
        try
        {
            await _incidentService.UpdateIncidentAsync(incidentDto);
            return NoContent();
        }
        catch (RecordNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{name}")]
    public async Task<IActionResult> DeleteIncident([FromQuery] string name)
    {
        try
        {
            await _incidentService.DeleteIncidentAsync(name);
            return NoContent();
        }
        catch (RecordNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

}