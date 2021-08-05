using System;
using System.Collections.Generic;
using DangEasy.Eventbrite.Builders;
using DangEasy.Eventbrite.Constants;
using DangEasy.Eventbrite.Models.Request;
using Library.Tests.Extensions;
using Xunit;

namespace Library.Tests
{
    public class When_Creating : BaseEventSetup
    {
        [Fact]
        public void Event_Is_Created()
        {
            var res = Event; // created in base class

            Assert.NotNull(res);

            Assert.Equal(Data_StartUtc.Date, res.Start.Utc.Date);
            Assert.Equal(Data_StartUtc.Hour, res.Start.Utc.Hour);
            Assert.Equal(Data_StartUtc.Minute, res.Start.Utc.Minute);
            Assert.Equal(Data_StartUtc.Second, res.Start.Utc.Second);

            Assert.Equal(Data_StartLocal.Date, res.Start.Local.Date);
            Assert.Equal(Data_StartLocal.Hour, res.Start.Local.Hour);
            Assert.Equal(Data_StartLocal.Minute, res.Start.Local.Minute);
            Assert.Equal(Data_StartLocal.Second, res.Start.Local.Second);
        }


        [Fact]
        public void StructuredDigitalContent_Is_Created()
        {
            var content = RequestModelBuilder.BuildStructuredDigitalContent(Request.StructuredDigitalContent.LiveStream, "Title", "https://us04web.zoom.us/j/1234567890");
            var res = Service.CreateStructuredDigitalContent(Event.Id, content).Result;

            Assert.NotNull(res);
        }


        [FactSkipWhenMockApi] // sales_start is null :(
        public void TicketClass_Has_Default_Dates_Created()
        {
            var expected_salesStartUtc = Data_ExecutionStartUtc;
            var expected_salesEndUtc = Event.Start.Utc.AddHours(-1); // by default, sales end 1hr before event start

            var model = RequestModelBuilder.BuildTicketClass("General Admission", 3, new List<string>() { Request.TicketClass.DeliveryMethodElectronic }, null, null, true);

            var res = Service.CreateTicketClass(Event.Id, model).Result;

            Assert.Equal(expected_salesStartUtc.Date, res.SalesStartUtc.Date);
            Assert.Equal(expected_salesStartUtc.Hour, res.SalesStartUtc.Hour);
            Assert.Equal(expected_salesStartUtc.Minute, res.SalesStartUtc.Minute);

            Assert.Equal(expected_salesEndUtc.Date, res.SalesEndUtc.Date);
            Assert.Equal(expected_salesEndUtc.Hour, res.SalesEndUtc.Hour);
            Assert.Equal(expected_salesEndUtc.Minute, res.SalesEndUtc.Minute);
        }


        [FactSkipWhenMockApi] // sales_start is null :(
        public void TicketClass_Has_Values_Created()
        {
            var salesStartUtc = Data_ExecutionStartUtc;
            var salesEndUtc = Event.Start.Utc.AddMinutes(-5);
            var model = RequestModelBuilder.BuildTicketClass("General Admission", 3, new List<string>() { Request.TicketClass.DeliveryMethodElectronic }, salesStartUtc, salesEndUtc, true);

            var res = Service.CreateTicketClass(Event.Id, model).Result;

            Assert.Equal("General Admission", res.Name);
            Assert.Equal(salesStartUtc.Date, res.SalesStartUtc.Date);
            Assert.Equal(salesStartUtc.Hour, res.SalesStartUtc.Hour);
            Assert.Equal(salesStartUtc.Minute, res.SalesStartUtc.Minute);
            //Assert.Equal(salesStartUtc.Second, res.SalesStartUtc.Second); // clocks are not in sync :/

            Assert.Equal(salesEndUtc.Date, res.SalesEndUtc.Date);
            Assert.Equal(salesEndUtc.Hour, res.SalesEndUtc.Hour);
            Assert.Equal(salesEndUtc.Minute, res.SalesEndUtc.Minute);
            //Assert.Equal(salesEndUtc.Second, res.SalesEndUtc.Second); // clocks are not in sync :/

            Assert.True(res.Free);
            Assert.Equal(1, res.MinimumQuantity);
            Assert.Equal(3, res.Capacity);
            Assert.Equal("electronic", res.DeliveryMethods[0]);
        }


        [FactSkipWhenMockApi] // cannot bind LiveStreamUrl  :(
        public void StructuredDigitalContent_Has_Values_Created()
        {
            const string title = "My Text";
            const string videoLink = "https://us04web.zoom.us/j/1234567890";

            var content = RequestModelBuilder.BuildStructuredDigitalContent(Request.StructuredDigitalContent.LiveStream, title, videoLink);
            var res = Service.CreateStructuredDigitalContent(Event.Id, content).Result;

            Assert.Equal(title, res.Modules[0].Data.LiveStreamUrl.Text);
            Assert.Equal(videoLink, res.Modules[0].Data.LiveStreamUrl.Url);
        }


        [FactSkipWhenMockApi] // Various properties are null - eg. Name
        public void Event_Has_Values_Created()
        {
            var res = Event; // created in base class

            Assert.Equal(Data_Title, res.Name.Text);
            Assert.Equal(Data_StartLocal.Date, res.Start.Local.Date);
            Assert.Equal(Data_StartLocal.Hour, res.Start.Local.Hour);
            Assert.Equal(Data_StartLocal.Minute, res.Start.Local.Minute);

            Assert.Equal(Data_StartUtc, res.Start.Utc);
            Assert.Equal(Data_EndUtc, res.End.Utc);

            Assert.Equal(Data_Timezone, res.Start.Timezone);
            Assert.Equal(Data_Timezone, res.End.Timezone);
            Assert.Equal(Data_Currency, res.Currency);
            Assert.True(res.OnlineEvent);
            Assert.True(res.Listed);
            Assert.True(res.Shareable);
        }


        [FactSkipWhenMockApi] // Body.Text is empty string
        public void StructuredContent_Is_Created()
        {
            var bodyText = "My Text";

            var content = RequestModelBuilder.BuildStructuredContentText(bodyText);
            var res = Service.CreateStructuredContent(Event.Id, content).Result;

            Assert.Equal(bodyText, res.Modules[0].Data.Body.Text);
        }
    }
}
