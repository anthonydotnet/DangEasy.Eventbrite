using System;
using System.Collections.Generic;
using DangEasy.Eventbrite.Builders;
using DangEasy.Eventbrite.Constants;
using DangEasy.Eventbrite.Models.Request;
using Library.Tests.Extensions;
using Xunit;

namespace Library.Tests
{
    public class When_Creating : BaseEventSetup, IDisposable
    {
        [Fact]
        public void Event_Is_Created()
        {
            var res = Event; // created in base class

            Assert.NotNull(res);
        }


        [FactSkipWhenMockApi]
        public void Event_Has_Values_Created()
        {
            var res = Event; // created in base class

            Assert.Equal(Data_Title, res.Name.Text);
            Assert.Equal(Data_StartUtc, res.Start.Local);
            Assert.Equal(Data_EndUtc, res.End.Local);
            Assert.Equal(Data_Timezone, res.Start.Timezone);
            Assert.Equal(Data_Timezone, res.End.Timezone);
            Assert.Equal(Data_StartUtc, res.Start.Utc);
            Assert.Equal(Data_EndUtc, res.End.Utc);
            Assert.Equal(Data_Currency, res.Currency);
            Assert.True(res.OnlineEvent);
            Assert.True(res.Listed);
            Assert.True(res.Shareable);
        }


        [Fact]
        public void StructuredContent_Is_Created()
        {
            var bodyText = ApiUrl.Contains("apiary-mock") ? string.Empty : "My Text"; // hack - mock is incomplete

            var content = RequestModelBuilder.BuildStructuredContentText(bodyText);
            var res = Service.CreateStructuredContent(Event.Id, content).Result;

            Assert.Equal(bodyText, res.Modules[0].Data.Body.Text);
        }


        [Fact]
        public void StructuredDigitalContent_Is_Created()
        {
            var content = RequestModelBuilder.BuildStructuredDigitalContent(Request.StructuredDigitalContent.LiveStream, "Title", "https://us04web.zoom.us/j/1234567890");
            var res = Service.CreateStructuredDigitalContent(Event.Id, content).Result;

            Assert.NotNull(res);
        }


        [FactSkipWhenMockApi] // cannot bind LiveStreamUrl  :(
        public void StructuredDigitalContent_Has_Values_Created()
        {
            var title = ApiUrl.Contains("apiary-mock") ? string.Empty : "My Text"; // hack - mock is incomplete
            const string videoLink = "https://us04web.zoom.us/j/1234567890";

            var content = RequestModelBuilder.BuildStructuredDigitalContent(Request.StructuredDigitalContent.LiveStream, title, videoLink);
            var res = Service.CreateStructuredDigitalContent(Event.Id, content).Result;

            Assert.Equal(title, res.Modules[0].Data.LiveStreamUrl.Text);
            Assert.Equal(videoLink, res.Modules[0].Data.LiveStreamUrl.Url);
        }


        [FactSkipWhenMockApi] // sales_start is null :(
        public void TicketClass_Has_Values_Created()
        {
            var executionStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 30, 0, 0);
            var salesStartUtc = executionStart.ToUniversalTime();
            var salesEndUtc = salesStartUtc.AddHours(1).ToUniversalTime();

            var model = RequestModelBuilder.BuildTicketClass("General Admission", 3, new List<string>() { "electronic" }, salesStartUtc, salesEndUtc, true);

            var res = Service.CreateTicketClass(Event.Id, model).Result;

            Assert.Equal("General Admission", res.Name);

            Assert.Equal(salesStartUtc.Date, res.SalesStart.Date);
            Assert.Equal(salesStartUtc.Hour, res.SalesStart.Hour);
            Assert.Equal(salesStartUtc.Minute, res.SalesStart.Minute); 
            Assert.Equal(salesStartUtc.Second, res.SalesStart.Second);

            Assert.Equal(salesEndUtc.Date, res.SalesEnd.Date);
            Assert.Equal(salesEndUtc.Hour, res.SalesEnd.Hour);
            Assert.Equal(salesEndUtc.Minute, res.SalesEnd.Minute);
            Assert.Equal(salesEndUtc.Second, res.SalesEnd.Second);

            Assert.True(res.Free);
            Assert.Equal(1, res.MinimumQuantity);
            //Assert.Equal(3, res.MaximumQuantity);
            Assert.Equal(3, res.Capacity);
            Assert.Equal("electronic", res.DeliveryMethods[0]);
        }
    }
}
