using System.Collections.Generic;
using Newtonsoft.Json;

namespace DangEasy.Eventbrite.Models.Response
{
    public class StructuredContent
    {
        [JsonProperty("page_version_number")]
        public int PageVersionNumber { get; set; }

        [JsonProperty("modules")]
        public List<Module> Modules { get; set; }
    }


    public class Module
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public class Data
    {
        [JsonProperty("body")]
        public Body Body { get; set; }
    }

    public class Body
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
