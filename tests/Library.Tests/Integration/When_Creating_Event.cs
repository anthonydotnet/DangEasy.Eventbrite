using System;
using DangEasy.Eventbrite.Builders;
using DangEasy.Eventbrite.Constants;
using Xunit;

namespace Eventbrite.Test.Integration
{
    public class When_Creating_Event : BaseEventSetup, IDisposable
    {       
        [Fact]
        public void Event_Is_Created()
        {
            var res = Event; // created in base class

            Assert.Equal(EventHelper.Title, res.Name.Text);
            Assert.Equal(EventHelper.StartUtc, res.Start.Local);
            Assert.Equal(EventHelper.EndUtc, res.End.Local);
            Assert.Equal(EventHelper.Timezone, res.Start.Timezone);
            Assert.Equal(EventHelper.Timezone, res.End.Timezone);
            Assert.Equal(EventHelper.StartUtc, res.Start.Utc);
            Assert.Equal(EventHelper.EndUtc, res.End.Utc);
            Assert.Equal(EventHelper.Currency, res.Currency);
            Assert.True(res.OnlineEvent);
            Assert.True(res.Listed);
            Assert.True(res.Shareable);
        }

    }
}
