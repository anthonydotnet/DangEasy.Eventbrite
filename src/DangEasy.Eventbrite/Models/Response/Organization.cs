using Newtonsoft.Json;

namespace DangEasy.Eventbrite.Models.Response
{
    public class Organization
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }
}
