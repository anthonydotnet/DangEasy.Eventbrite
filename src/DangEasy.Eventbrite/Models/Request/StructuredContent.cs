using System.Collections.Generic;
using Newtonsoft.Json;

namespace DangEasy.Eventbrite.Models.Request
{
    public class StructuredContent
    {
        public const string Listing = "listing";
        public const string DigitalContent = "digital_content";

        [JsonProperty("modules")]
        public List<Module> Modules { get; set; }

        [JsonProperty("publish")]
        public bool Publish { get; set; }

        [JsonProperty("purpose")]
        public string Purpose { get; set; } // listing / digital_content
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

