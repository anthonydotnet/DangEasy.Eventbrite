using System;
using DangEasy.Eventbrite.Services;
using DangEasy.Eventbrite.Models.Response;
using Xunit;
using DangEasy.Configuration;

namespace Eventbrite.Test.Integration
{
    public abstract class BaseEventSetup : IDisposable
    {
        protected IEventbriteService _service;
        protected Event Event;
        public EventSetup EventHelper;
        public BaseEventSetup()
        {
            var config = new ConfigurationLoader().Load("appsettings.json", Directory.Bin);
            _service = new EventbriteService(config["Values:Eventbrite_ApiUrl"], config["Values:Eventbrite_Token"]);

            EventHelper = new EventSetup(_service);
            Event = EventHelper.Create();
        }

        public void Dispose()
        {
            var res = _service.DeleteEvent(Event.Id).Result;

            Assert.True(res);
        }
    }
}
