using Newtonsoft.Json;

namespace DangEasy.Eventbrite.Models.Response
{
    public class EventCancelled
    {
        [JsonProperty("canceled")]
        public bool Canceled { get; set; }
    }
}
