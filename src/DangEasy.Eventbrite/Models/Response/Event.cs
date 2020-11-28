using System;
using DangEasy.Eventbrite.Models.Shared;
using Newtonsoft.Json;

namespace DangEasy.Eventbrite.Models.Response
{
    public class Event
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public TextHtml Name { get; set; }

        [JsonProperty("summary")]
        public TextHtml Summary { get; set; }

        [JsonProperty("description")]
        public TextHtml Description { get; set; }

        [JsonProperty("start")]
        public EventDate Start { get; set; }

        [JsonProperty("end")]
        public EventDate End { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("vanity_url")]
        public string VanityUrl { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("changed")]
        public DateTime Changed { get; set; }

        [JsonProperty("published")]
        public DateTime Published { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("online_event")]
        public bool OnlineEvent { get; set; }

        // these need to be strings because the mock server  returns empty string :(
        [JsonProperty("organization_id")]
        public string OrganizationId { get; set; }

        [JsonProperty("organizer_id")]
        public string OrganizerId { get; set; }

        //organizer
        //logo_id
        //logo
        //venue_id
        //format_id
        //format
        //category_id
        //category
        //subcategory_id
        //subcategory
        //music_properties
        //bookmark_info
        //refund_policy
        //ticket_availability



        // private fields
        [JsonProperty("listed")]
        public bool Listed { get; set; }

        [JsonProperty("shareable")]
        public bool Shareable { get; set; }

        //[JsonProperty("invite_only")]
        //public bool InviteOnly { get; set; }

        //[JsonProperty("show_remaining")]
        //public bool ShowRemaining { get; set; }

        //[JsonProperty("password")]
        //public string Password { get; set; }

        [JsonProperty("capacity")]
        public int Capacity { get; set; }

        //capacity_is_custom
        //tx_time_limit

        //[JsonProperty("hide_start_date")]
        //public bool HideStartDate { get; set; }

        //[JsonProperty("hide_end_date")]
        //public bool HideEndDate { get; set; }

        
       [JsonProperty("locale")]
        public string Locale { get; set; }

        //is_locked
        //privacy_setting
        //is_externally_ticketed
        //external_ticketing
        //is_series
        //is_series_parent
        //series_id
        //is_reserved_seating
        //show_pick_a_seat
        //show_seatmap_thumbnail
        //show_colors_in_seatmap_thumbnail

        
        [JsonProperty("is_free")]
        public bool IsFree { get; set; }

        //source
        //version

       [JsonProperty("resource_uri")]
        public string ResourceUri { get; set; }

        //event_sales_status
        //checkout_settings
    }
}
