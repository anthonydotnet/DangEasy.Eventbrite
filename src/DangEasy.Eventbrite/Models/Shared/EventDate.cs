using System;
using Newtonsoft.Json;

namespace DangEasy.Eventbrite.Models.Shared
{
    public class EventDate
    {
        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("local")]
        public DateTime Local { get; set; }

        [JsonProperty("utc")]
        public DateTime Utc { get; set; }
    }
}
