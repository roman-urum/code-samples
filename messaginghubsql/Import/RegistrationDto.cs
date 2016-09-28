using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Import
{
    public enum RegistrationType { APN = 0, GCM = 1, VCS = 2, FCG = 3, SMS = 4 }

    public class RegistrationDto
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "secret")]
        public string Secret { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public IEnumerable<string> Tags { get; set; }

        [JsonProperty(PropertyName = "type")]
        public RegistrationType Type { get; set; }

        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "application")]
        public string Application { get; set; }

        [JsonProperty(PropertyName = "device")]
        public string Device { get; set; }

        [JsonProperty(PropertyName = "verified")]
        public bool Verified { get; set; }

        [JsonProperty(PropertyName = "verificationCode")]
        public string VerificationCode { get; set; }

        [JsonProperty(PropertyName = "disabled")]
        public bool Disabled { get; set; }
    }
}
