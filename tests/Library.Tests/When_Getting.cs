using Xunit;
using DangEasy.Eventbrite.Models.Request;
using System;
using DangEasy.Eventbrite.Builders;
using System.Linq;
using Library.Tests.Extensions;
using DangEasy.Eventbrite.Constants;

namespace Library.Tests
{
    public class When_Getting : BaseEventSetup, IDisposable
    {
        [FactSkipWhenMockApi]
        public void Events_Are_Retrieved()
        {
            var orgId = Event.OrganizationId;

            var res = Service.GetEvents(orgId).Result;

            Assert.NotEmpty(res.Events);
        }


        [Fact]
        public void Event_Is_Retrieved()
        {
            var res = Service.GetEvent(Event.Id).Result;

            Assert.True(res.Id > 1);
        }

        [Fact]
        public void StructuredContent_Is_Not_Retrieved()
        {
            var res = Service.GetStructuredContent(Event.Id).Result;

            // Assert.Null(1, res.PageVersionNumber);
            Assert.Null(res.PageVersionNumber);
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


        [Fact]
        public void StructuredDigitalContent_Is_Not_Retrieved()
        {
            var res = Service.GetStructuredDigitalContent(Event.Id).Result;

            Assert.DoesNotContain(res.Modules, x => x.Purpose == Request.StructuredDigitalContent.Purpose);
        }

    }
}
