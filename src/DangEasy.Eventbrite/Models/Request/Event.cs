using System;
using DangEasy.Eventbrite.Models.Shared;
using Newtonsoft.Json;

namespace DangEasy.Eventbrite.Models.Request
{
    public class Event
    {
        [JsonProperty("event.name.html")]
        public string Name { get; set; }


        //[JsonProperty("description")]
        //public TextHtml Description { get; set; }

        [JsonProperty("event.start.utc")]
        public DateTime StartUtc { get; set; }

        [JsonProperty("event.end.utc")]
        public DateTime EndUtc { get; set; }

        [JsonProperty("event.start.timezone")]
        public string StartTimezone { get; set; }

        [JsonProperty("event.end.timezone")]
        public string EndTimezone { get; set; }

        [JsonProperty("event.currency")]
        public string Currency { get; set; }


        [JsonProperty("event.online_event")]
        public bool OnlineEvent { get; set; }

        //venue_id
        //category_id
        //subcategory_id

        [JsonProperty("event.listed")]
        public bool Listed { get; set; }

        [JsonProperty("event.shareable")]
        public bool Shareable { get; set; }

        //invite_only
        //show_remaining
        //password
        

        //[JsonProperty("event.capacity")]
        //public bool Capacity { get; set; }

        //is_reserved_seating
        //is_series
        //show_pick_a_seat
        //show_seatmap_thumbnail
        //show_colors_in_seatmap_thumbnail
        //source
        //locale - Default: en_US

    }
}
