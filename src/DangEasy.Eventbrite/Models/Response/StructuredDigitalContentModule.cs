using Newtonsoft.Json;

namespace DangEasy.Eventbrite.Models.Response
{
    public class StructuredDigitalContentModule
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("data")]
        public DigitalContentData Data { get; set; }

        [JsonProperty("purpose")]
        public string Purpose { get; set; }
    }


    public class DigitalContentData
    {
        [JsonProperty("livestream_url")]
        public LiveStreamContentBody LiveStreamUrl { get; set; }
    }

    public class LiveStreamContentBody
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
