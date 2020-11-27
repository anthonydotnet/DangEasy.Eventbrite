using Xunit;
using DangEasy.Eventbrite.Models.Request;
using System;
using DangEasy.Eventbrite.Builders;

namespace Eventbrite.Test.Integration
{
    public class When_Getting_Event : BaseEventSetup, IDisposable
    {
        [Fact]
        public void Event_Is_Retrieved()
        {
            var res = _service.GetEvent(Event.Id).Result;

            Assert.True(res.Id > 1);
        }

        [Fact]
        public  void Description_Is_Retrieved()
        {
            // setup
            var structuredContent = RequestModelBuilder.BuildStructuredContent("My description", "text");

            var res = _service.CreateStructuredContent(Event.Id, structuredContent).Result;

           // var res =  _service.GetStructuredContent(_event.Id, StructuredContent.Listing).Result;

            Assert.Equal("My description", res.Modules[0].Data.Body.Text);
            Assert.True(res.PageVersionNumber >=1);
        }


        /*
        [Fact]
        public void DigitalContent_Is_Retrieved()
        {
            _ = _service.AddDigitalContent(_event.Id, "Video Link", "https://zoom.us/123456789/");
            var res = _service.GetStructuredDigitalContent(_event.Id, StructuredContent.DigitalContent).Result;

            Assert.True(res.PageVersionNumber > 0);
            Assert.Equal("Video Link", res.Modules[0].Data.Body.Text);
            Assert.Equal("https://zoom.us/123456789/", res.Modules[0].Data.Body.Url);
        }


        [Fact]
        public void TicketClass_Is_Retrieved()
        {
            var res = _service.GetTicketClass(_event.Id).Result;

            Assert.True(res.TicketClasses.Count > 0);
        }
        */

    }
}
