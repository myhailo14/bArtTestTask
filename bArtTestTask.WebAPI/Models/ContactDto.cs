using System.Text.Json.Serialization;

namespace bArtTestTask.WebAPI.Models;

public class ContactDto
{
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }
    [JsonPropertyName("lastName")]
    public string LastName { get; set; }
}