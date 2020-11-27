using Newtonsoft.Json;

namespace DangEasy.Eventbrite.Models.Response
{
    // TODO: Use this class!
    public class Error
    {
        [JsonProperty("status_code")]
        public string StatusCode { get; set;}

        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }

        [JsonProperty("error")]
        public string ErrorType { get; set; }
    }
}
