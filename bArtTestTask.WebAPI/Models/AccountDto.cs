using System.Text.Json.Serialization;

namespace bArtTestTask.WebAPI.Models;

public class AccountDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("incidentName")]
    public string IncidentName { get; set; }
}