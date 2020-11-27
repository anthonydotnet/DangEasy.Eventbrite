using System.Collections.Generic;
using Newtonsoft.Json;

namespace DangEasy.Eventbrite.Models.Response.Paginated
{
    public class OrganizationPaginated
    {
        [JsonProperty("organizations")]
        public List<Organization> Organizations { get; set; }

        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
    }
}
