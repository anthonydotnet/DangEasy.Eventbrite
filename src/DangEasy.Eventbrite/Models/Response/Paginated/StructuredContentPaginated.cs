using System.Collections.Generic;
using Newtonsoft.Json;

namespace DangEasy.Eventbrite.Models.Response.Paginated
{
    public class StructuredContentPaginated
    {
        [JsonProperty("page_version_number")]
        public int ? PageVersionNumber { get; set; }

        [JsonProperty("access_type")]
        public string AccessType { get; set; }

        [JsonProperty("modules")]
        public List<StructuredContentModule> Modules { get; set; }

        [JsonProperty("purpose")]
        public string Purpose { get; set; }

        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
    }
}
