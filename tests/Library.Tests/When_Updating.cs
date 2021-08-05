using System;
using Xunit;
using DangEasy.Eventbrite.Models.Request;
using System.Collections.Generic;
using DangEasy.Eventbrite.Builders;
using Library.Tests.Extensions;

namespace Library.Tests
{
    public class When_Updating : BaseEventSetup, IDisposable
    {
        [Fact]
        public void Event_Description_Is_Updated()
        {
            // setup
            var structuredContent = RequestModelBuilder.BuildStructuredContentText("My description");
            var structuredContent2 = RequestModelBuilder.BuildStructuredContentText("My description2");

            var content = Service.CreateStructuredContent(Event.Id, structuredContent).Result;
            var res = Service.CreateStructuredContent(Event.Id, structuredContent2).Result;

            Assert.True(res.PageVersionNumber == content.PageVersionNumber + 1);
        }


        [FactSkipWhenMockApi] // organisationId is empty string :(
        public void Event_Is_Updated()
        {
            // setup
            var updatedStartUtc = Event.Start.Utc.AddHours(1);
            var updatedEndUtc = Event.End.Utc.AddHours(1);

            var @event = RequestModelBuilder.BuildEvent("New Title", updatedStartUtc, updatedEndUtc, "Australia/Sydney", "AUD");

            var res = Service.UpdateEvent(Event.Id, @event).Result;

            Assert.NotEmpty(res.Name.Text);
            Assert.True(res.Start.Utc > DateTime.MinValue);
            Assert.True(res.End.Utc > DateTime.MinValue);
            Assert.NotEmpty( res.Start.Timezone);
            Assert.NotEmpty( res.End.Timezone);
            Assert.NotEmpty( res.Currency);
        }



        [FactSkipWhenMockApi] // organisationId is empty string :(
        public void Event_Property_Is_Updated()
        {
            const long logoId = 120775453;

            var res = Service.UpdateEvent(Event.Id, "event.logo_id", logoId.ToString()).Result;

            Assert.Equal(logoId, res.LogoId);
        }



        [FactSkipWhenMockApi] // organisationId is empty string :(
        public void Event_Details_Are_Updated()
        {
            // setup
            var updatedTitle = "New Title";
            var updatedStartUtc = Event.Start.Utc.AddHours(1);
            var updatedEndUtc = Event.End.Utc.AddHours(1);
            var updatedTimezone = "Australia/Sydney";
            var updatedCurrency = "AUD";

            var @event = RequestModelBuilder.BuildEvent(updatedTitle, updatedStartUtc, updatedEndUtc, updatedTimezone, updatedCurrency);

            var res = Service.UpdateEvent(Event.Id, @event).Result;

            Assert.Equal(updatedTitle, res.Name.Text);
            Assert.Equal(updatedStartUtc, res.Start.Utc);
            Assert.Equal(updatedEndUtc, res.End.Utc);
            Assert.Equal(updatedTimezone, res.Start.Timezone);
            Assert.Equal(updatedTimezone, res.End.Timezone);
            Assert.Equal(updatedCurrency, res.Currency);
        }


        [FactSkipWhenMockApi] // ticket class sales_start is null :(
        public void Event_Publishing_Succeeds()
        {
            var ticketClass = RequestModelBuilder.BuildTicketClass("General Admission", 3, new List<string>() { "electronic" });

            _ = Service.CreateTicketClass(Event.Id, ticketClass).Result;

            var res = Service.PublishEvent(Event.Id).Result;

            Assert.True(res);
        }


        [FactSkipWhenMockApi] // ticket class sales_start is null :(
        public void Event_Unpublishing_Succeeds()
        {
            var ticketClass = RequestModelBuilder.BuildTicketClass("General Admission", 3, new List<string>() { "electronic" });
            _ = Service.CreateTicketClass(Event.Id, ticketClass).Result;
            _ = Service.PublishEvent(Event.Id).Result;

            var res = Service.UnPublishEvent(Event.Id).Result;

            Assert.True(res);
        }
    }
}
