using System;
using DangEasy.Eventbrite.Builders;
using DangEasy.Eventbrite.Models.Response;
using DangEasy.Eventbrite.Services;
using Xunit;

namespace Eventbrite.Test
{
    public class EventSetup : IDisposable
    {
        IEventbriteService _service;
        Event _event;

        public DateTime ExecutionStart;
        public DateTime StartUtc;
        public DateTime EndUtc;
        public string Timezone = "Europe/London";
        public string Title = "My Event";
        public string Currency = "USD";
        public bool OnlineEvent = true;
        public bool Listed = true;
        public bool Shareable = true;

        public EventSetup(IEventbriteService service)
        {
            _service = service;

            ExecutionStart = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, DateTime.UtcNow.Second, 0);
        }

        public Event Create()
        {
            StartUtc = ExecutionStart.AddHours(2);
            EndUtc = StartUtc.AddHours(1);

            var @event = RequestModelBuilder.BuildEvent(Title, StartUtc, EndUtc, Timezone, Currency);

            _event = _service.CreateEvent(@event).Result;

            return _event;
        }


        public void Dispose()
        {
            var res = _service.DeleteEvent(_event.Id).Result;

            Assert.True(res);
        }
    }
}
