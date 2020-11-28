using Newtonsoft.Json;

namespace DangEasy.Eventbrite.Models.Response
{
    public class StructuredContentModule
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

        // Pondering if image and video properties should live here? Should Data be IData?
        /*
         "image": {
                    "image_id": "",
                    "display_size": "small",
                    "corner_style": "rounded",
                    "alt": ""
                },
                "caption": {
                    "body": {
                        "alignment": "left",
                        "text": ""
                    }
                },
        */

        /*
        "video": {
                    "display_size": "",
                    "embed_url": "",
                    "thumbnail_url": "",
                    "url": ""
                },
                "caption": {
                    "body": {
                        "alignment": "left",
                        "text": ""
                    }
                }
         */

    }

    public class Body
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
