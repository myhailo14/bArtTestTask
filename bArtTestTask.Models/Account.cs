using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace bArtTestTask.Models;

public class Account
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Incident? Incident { get; set; }
    [JsonIgnore]
    public ICollection<Contact> Contacts { get; set; }
}