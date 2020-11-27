using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DangEasy.Eventbrite.Models.Response
{
    public class TicketClass
    {
        //[JsonProperty("resource_uri")]
        //public string ResourceUri { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        //[JsonProperty("description")]
        //public string Description { get; set; }

        [JsonProperty("sales_start")]
        public DateTime SalesStart { get; set; }

        [JsonProperty("sales_end")]
        public DateTime SalesEnd { get; set; }


        [JsonProperty("free")]
        public bool Free { get; set; }

        [JsonProperty("minimum_quantity")]
        public int? MinimumQuantity { get; set; }

        [JsonProperty("maximum_quantity")]
        public int? MaximumQuantity { get; set; }

        [JsonProperty("capacity")]
        public int Capacity { get; set; }

        [JsonProperty("delivery_methods")]
        public List<string> DeliveryMethods { get; set; }
    }
}
