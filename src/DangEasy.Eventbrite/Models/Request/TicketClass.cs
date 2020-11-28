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


        //[JsonProperty("ticket_class.cost")]
        //public Currency Cost { get; set; }

        //[JsonProperty("ticket_class.minimum_quantity")]
        //public int MinimumQuantity { get; set; }

        [JsonProperty("ticket_class.maximum_quantity")]
        public int ? MaximumQuantity { get; set; }

        [JsonProperty("ticket_class.capacity")]
        public int Capacity { get; set; }

        [JsonProperty("ticket_class.delivery_methods")]
        public List<string> DeliveryMethods { get; set; }


        [JsonProperty("ticket_class.sales_start")]
        public DateTime ? SalesStartUtc { get; set; }

        [JsonProperty("ticket_class.sales_end")]
        public DateTime? SalesEndUtc { get; set; }
    }


    //public class Currency
    //{
    //}

    /*
         {
            "currency": "USD",
            "value": 432,
            "major_value": "4.32",
            "display": "4.32 USD"
         }
        */
}
