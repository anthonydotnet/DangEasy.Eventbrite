using System.Collections.Generic;
using Newtonsoft.Json;

namespace DangEasy.Eventbrite.Models.Response.Paginated
{
    public class EventPaginated
    {
        [JsonProperty("events")]
        public List<Event> Events { get; set; }

        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
    }
}
