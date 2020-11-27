using System.Collections.Generic;
using Newtonsoft.Json;

namespace DangEasy.Eventbrite.Models.Request
{
    public class StructuredDigitalContent
    {       
        [JsonProperty("modules")]
        public List<DigitalModule> Modules { get; set; }

        [JsonProperty("publish")]
        public bool Publish { get; set; }

        [JsonProperty("purpose")]
        public string Purpose { get; set; } // listing / digital_content
    }


    public class DigitalModule
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

