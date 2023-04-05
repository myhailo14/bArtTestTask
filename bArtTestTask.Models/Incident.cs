using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace bArtTestTask.Models
{
    public class Incident
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public ICollection<Account> Accounts { get; set; }
    }
}