using System.Collections.Generic;
using Newtonsoft.Json;

namespace DangEasy.Eventbrite.Models.Response.Paginated
{
    public class StructuredDigitalContentPaginated
    {
        [JsonProperty("page_version_number")]
        public int ? PageVersionNumber { get; set; }

        [JsonProperty("modules")]
        public List<StructuredDigitalContentModule> Modules { get; set; }

        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
    }

}
