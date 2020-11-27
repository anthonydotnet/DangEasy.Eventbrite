using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DangEasy.Eventbrite.Models.Request
{
    public class TicketClass
    {
        [JsonProperty("ticket_class.name")]
        public string Name { get; set; }

        [JsonProperty("ticket_class.free")]
        public bool Free { get; set; }

        //[JsonProperty("ticket_class.minimum_quantity")]
        //public int MinimumQuantity { get; set; }

        //[JsonProperty("ticket_class.maximum_quantity")]
        //public int MaximumQuantity { get; set; }

        [JsonProperty("ticket_class.capacity")]
        public int Capacity { get; set; }

        [JsonProperty("ticket_class.delivery_methods")]
        public List<string> DeliveryMethods { get; set; }
        

        //[JsonProperty("ticket_class.sales_start")]
        //public DateTime SalesStart { get; set; }

        //[JsonProperty("ticket_class.sales_end")]
        //public DateTime SalesEnd { get; set; }
    }
}
