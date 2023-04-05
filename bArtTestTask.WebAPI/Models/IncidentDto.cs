using System.Text.Json.Serialization;
using bArtTestTask.Models;

namespace bArtTestTask.WebAPI.Models;

public class IncidentDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
}