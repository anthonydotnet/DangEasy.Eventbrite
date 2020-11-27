using System;
using Xunit;
using DangEasy.Eventbrite.Models.Request;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using DangEasy.Eventbrite.Builders;
using DangEasy.Eventbrite.Constants;

namespace Eventbrite.Test.Integration
{
    public class When_Updating_Event : BaseEventSetup, IDisposable
    {
        
        [Fact]
        public void TicketClass_Is_Added()
        {
            var executionStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, 0);
            var salesStartUtc = executionStart.AddMinutes(1).ToUniversalTime();
            var salesEndUtc = salesStartUtc.AddHours(1).ToUniversalTime();

            var ticketClass = new TicketClass { Name = "General Admission", Capacity = 3, DeliveryMethods = new List<string>() { "electronic" }, Free = true };

            var res = _service.CreateTicketClass(Event.Id, ticketClass).Result;

            Assert.Equal("General Admission", res.Name);

            Assert.Equal(salesStartUtc.Date, res.SalesStart.Date);
            Assert.Equal(salesStartUtc.Hour, res.SalesStart.Hour);
            Assert.Equal(salesStartUtc.Minute, res.SalesStart.Minute);
            Assert.Equal(salesStartUtc.Second, res.SalesStart.Second);
            Assert.Equal(salesEndUtc, res.SalesEnd); // ??? what should we do here?

            Assert.True(res.Free);
            Assert.Equal(1, res.MinimumQuantity);
            Assert.Equal(3, res.MaximumQuantity);
            Assert.Equal(3, res.Capacity);
            Assert.Equal("electronic", res.DeliveryMethods[0]);
        }


        [Fact]
        public void Event_Description_Is_Updated()
        {
            // setup
            var structuredContent = RequestModelBuilder.BuildStructuredContent("My description", "text");
            var structuredContent2 = RequestModelBuilder.BuildStructuredContent("My description2", "text");

            var content = _service.CreateStructuredContent(Event.Id, structuredContent).Result;
            var res = _service.CreateStructuredContent(Event.Id, structuredContent2).Result;

            Assert.True(res.PageVersionNumber == content.PageVersionNumber + 1);
        }

        [Fact]
        public void StructuredContent_Is_Created()
        {
            var content = RequestModelBuilder.BuildStructuredContent(Constants.StructuredContent.Text, "Title");
            var res = _service.CreateStructuredContent(Event.Id, content).Result;

            Assert.Equal("Title", res.Modules[0].Data.Body.Text);
        }


        [Fact]
        public void StructuredDigitalContent_Is_Created()
        {
            var content = RequestModelBuilder.BuildStructuredDigitalContent(Constants.StructuredDigitalContent.LiveStream, "Title", "https://us04web.zoom.us/j/1234567890");
            var res = _service.CreateStructuredDigitalContent(Event.Id, content).Result;

            Assert.Equal("Title", res.Modules[0].Data.Body.Text);
        }



        [Fact]
        public void Event_Details_Are_Updated()
        {
            // setup
            var updatedTitle = "New Title";
            var updatedStartUtc = Event.Start.Utc.AddHours(1);
            var updatedEndUtc = Event.End.Utc.AddHours(1);
            var updatedTimezone = "Australia/Sydney";
            var updatedCurrency = "AUD";

            var @event = RequestModelBuilder.BuildEvent(updatedTitle, updatedStartUtc, updatedEndUtc, updatedTimezone, updatedCurrency);

            var res = _service.UpdateEvent(Event.Id, @event).Result;

            Assert.Equal(updatedTitle, res.Name.Text);
            Assert.Equal(updatedStartUtc, res.Start.Utc);
            Assert.Equal(updatedEndUtc, res.End.Utc);
            Assert.Equal(updatedTimezone, res.Start.Timezone);
            Assert.Equal(updatedTimezone, res.End.Timezone);
            Assert.Equal(updatedCurrency, res.Currency);
        }


        [Fact]
        public void Event_Publishing_Succeeds()
        {
            var res = _service.PublishEvent(Event.Id).Result;

            Assert.True(res);
        }


        [Fact]
        public void Event_Unpublishing_Succeeds()
        {
            var res = _service.UnPublishEvent(Event.Id).Result;

            Assert.True(res);
        }
    }
}
