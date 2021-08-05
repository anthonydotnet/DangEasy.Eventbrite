using Xunit;
using DangEasy.Eventbrite.Models.Request;
using System;
using DangEasy.Eventbrite.Builders;
using Library.Tests.Extensions;
using DangEasy.Eventbrite.Constants;

namespace Library.Tests
{
    public class When_Getting : BaseEventSetup, IDisposable
    {
        [FactSkipWhenMockApi] // PageVersionNumber is null :(
        public void StructuredContent_Is_Not_Retrieved()
        {
            var res = Service.GetStructuredContent(Event.Id).Result;

            Assert.Equal(1, res.PageVersionNumber);
            Assert.Empty(res.Modules);
        }


        [Fact]
        public void StructuredContent_Is_Retrieved()
        {
            var bodyText = ApiUrl.Contains("apiary-mock") ? string.Empty : "My Text"; // hack - mock is incomplete 

            // setup
            var structuredContent = RequestModelBuilder.BuildStructuredContentText(bodyText);
            _ = Service.CreateStructuredContent(Event.Id, structuredContent).Result;

            var res = Service.GetStructuredContent(Event.Id).Result;

            Assert.Equal(bodyText, res.Modules[0].Data.Body.Text);
        }


        [FactSkipWhenMockApi] // Models is not null :(
        public void StructuredDigitalContent_Is_Not_Retrieved()
        {
            var res = Service.GetStructuredDigitalContent(Event.Id).Result;

            Assert.Null(res.Modules); // yes it is null for digital content :(
        }


        [FactSkipWhenMockApi] // Data is null :(
        public void StructuredDigitalContent_Is_Retrieved()
        {
            const string title = "My Text";
            const string videoLink = "https://us04web.zoom.us/j/1234567890";
            var content = RequestModelBuilder.BuildStructuredDigitalContent(Request.StructuredDigitalContent.LiveStream, title, videoLink);
            _ = Service.CreateStructuredDigitalContent(Event.Id, content).Result;

            var res = Service.GetStructuredDigitalContent(Event.Id).Result;

            Assert.Equal(title, res.Modules[0].Data.LiveStreamUrl.Text);
            Assert.Equal(videoLink, res.Modules[0].Data.LiveStreamUrl.Url);
        }


        [FactSkipWhenMockApi] // organisationId is empty string :(
        public void Events_Are_Retrieved()
        {
            var orgId = Event.OrganizationId;

            var res = Service.GetEvents(orgId).Result;

            Assert.NotEmpty(res.Events);
        }


        [FactSkipWhenMockApi] // organisationId is empty string :(
        public void Event_Is_Retrieved()
        {
            var res = Service.GetEvent(Event.Id).Result;

            Assert.True(res.Id > 1);
        }
    }
}
