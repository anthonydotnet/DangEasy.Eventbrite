using Xunit;
using DangEasy.Eventbrite.Models.Request;
using System;
using DangEasy.Eventbrite.Builders;
using System.Linq;

namespace Library.Tests
{
    public class When_Getting : BaseEventSetup, IDisposable
    {
        [Fact]
        public void Events_Are_Retrieved()
        {
            var res = Service.GetEvents(long.Parse(Event.OrganizationId)).Result;

            Assert.True(res.Events.Count() > 1);
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

            Assert.Equal(1, res.PageVersionNumber);
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
            //Assert.Equal("My image", res.Modules[1].Data.Image.Text);
            Assert.True(res.PageVersionNumber >= 1);
        }


        [Fact]
        public void StructuredDigitalContent_Is_Not_Retrieved()
        {
            var res = Service.GetStructuredDigitalContent(Event.Id).Result;

            Assert.Null(res.PageVersionNumber);
        }

        /*  [Fact]
          public void DigitalContent_Is_Retrieved()
          {
              var content = RequestModelBuilder.BuildStructuredDigitalContent("My text");

              _ = _service.CreateStructuredDigitalContent(Event.Id, "Video Link", "https://zoom.us/123456789/");

              var res = _service.GetStructuredDigitalContent(Event.Id, Request.StructuredContent.Purpose).Result;

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
