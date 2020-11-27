using Newtonsoft.Json;

namespace DangEasy.Eventbrite.Models.Shared
{
    public class TextHtml
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("html")]
        public string Html { get; set; }
    }
}
