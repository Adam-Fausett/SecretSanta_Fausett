using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SecretSanta.Models
{
    public class Participant
    {
        [JsonIgnore, XmlIgnore]
        public  Guid ID { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
    }
}
