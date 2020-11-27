using System;
using System.Collections.Generic;
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


        public static StructuredContent BuildStructuredContent(string type, string title, bool publish = true)
        {
            var model = new StructuredContent
            {
                Purpose = Constants.Constants.StructuredContent.Purpose,
                Modules = new List<Module>
                {
                    {
                        new Module
                        {
                            Data = new Data
                            {
                                Body = new Body
                                {
                                    Text = title
                                }
                            },
                            Type = type // "text"
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
                Purpose = Constants.Constants.StructuredDigitalContent.Purpose,
                Modules = new List<DigitalModule>
                {
                    {
                        new DigitalModule
                        {
                            Data = new DigitalContentData
                            {
                                Body = new DigitalContentBody
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
    }
}
