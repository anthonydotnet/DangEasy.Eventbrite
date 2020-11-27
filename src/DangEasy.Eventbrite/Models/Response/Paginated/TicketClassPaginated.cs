using System.Collections.Generic;
using Newtonsoft.Json;

namespace DangEasy.Eventbrite.Models.Response.Paginated
{
    public class TicketClassPaginated
    {
        [JsonProperty("ticket_classes")]
        public List<TicketClass> TicketClasses { get; set; }

        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
    }
}
