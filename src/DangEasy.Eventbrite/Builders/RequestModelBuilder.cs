using System;
using System.Collections.Generic;
using DangEasy.Eventbrite.Constants;
using DangEasy.Eventbrite.Models.Request;

namespace DangEasy.Eventbrite.Builders
{
    public class RequestModelBuilder
    {
        // creates a simple event - do we need this?
        public static Event BuildEvent(string title, DateTime startUtc, DateTime endUtc, string timezone, string currency, bool onlineEvent = true, bool listed = true, bool shareable = true)
        {
            var model = new Event
            {
                Name = title,
                Start = startUtc,
                End = endUtc,
                StartTimezone = timezone,
                EndTimezone = timezone,
                Currency = currency,
                OnlineEvent = onlineEvent,
                Listed = listed,
                Shareable = shareable
            };

            return model;
        }


        public static StructuredContent BuildStructuredContentText(string text, bool publish = true)
        {
            var model = new StructuredContent
            {
                Purpose = Request.StructuredContent.Purpose,
                Modules = new List<Module>
                {
                    {
                        new Module
                        {
                            Data = new Data
                            {
                                Body = new Body
                                {
                                    Text = text
                                }
                            },
                            Type = Request.StructuredContent.Text
                        }
                    }
                },
                Publish = publish
            };

            return model;
        }



        public static StructuredDigitalContent BuildStructuredDigitalContent(string type, string title, string url, bool publish = true)
        {

            var model = new StructuredDigitalContent
            {
                Purpose = Request.StructuredDigitalContent.Purpose,
                Modules = new List<DigitalModule>
                {
                    {
                        new DigitalModule
                        {
                            Data = new DigitalContentData
                            {
                                LiveStreamUrl = new LiveStreamContentBody
                                {
                                    Text = title,
                                    Url = url
                                }
                            },
                            Type = type // "livestream" 
                        }
                    }
                },
                Publish = publish
            };

            return model;
        }


        public static TicketClass BuildTicketClass(string name, int capacity, List<string> deliveryMethods, DateTime ? salesStartUtc = null, DateTime ?salesEndUtc = null, bool isFree = true)
        {
            var model = new TicketClass
            {
                Name = name,
                Capacity = capacity,
                DeliveryMethods = deliveryMethods,
                Free = isFree,
                SalesStartUtc = salesStartUtc,
                SalesEndUtc = salesEndUtc
            };

            return model;
        }

    }
}
