using System.Collections.Generic;
using DangEasy.Eventbrite.Models.Request;
using Newtonsoft.Json;

namespace DangEasy.Eventbrite.Models.Response
{
    public class StructuredDigitalContent
    {
        [JsonProperty("page_version_number")]
        public int PageVersionNumber { get; set; }

        [JsonProperty("modules")]
        public List<DigitalContentModule> Modules { get; set; }
    }

    public class DigitalContentModule
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("data")]
        public DigitalContentData Data { get; set; }
    }

    public class DigitalContentData 
    {
        [JsonProperty("livestream_url")]
        public DigitalContentBody Body { get; set; }
    }

    public class DigitalContentBody
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
