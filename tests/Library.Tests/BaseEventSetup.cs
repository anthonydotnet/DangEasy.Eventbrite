using System;
using DangEasy.Eventbrite.Services;
using DangEasy.Eventbrite.Models.Response;
using Xunit;
using DangEasy.Configuration;
using Microsoft.Extensions.Configuration;
using DangEasy.Eventbrite.Builders;
using DangEasy.Eventbrite.Models.Shared;

namespace Library.Tests
{
    public abstract class BaseEventSetup : IDisposable
    {
        public IConfigurationRoot Config = new ConfigurationLoader().Load("appsettings.json", Directory.Bin);
        public string ApiUrl => Config["Values:Eventbrite_ApiUrl"];
        public string Token => Config["Values:Eventbrite_Token"];

        public IEventbriteService Service;

        public Event Event; // scaffolded event

        // event test data
        public DateTime Data_ExecutionStartUtc;
        public DateTime Data_StartLocal;
        public DateTime Data_EndLocal;

        public DateTime Data_StartUtc;
        public DateTime Data_EndUtc;
        public string Data_Timezone = "Europe/London";
        public string Data_Title = "My Event";
        public string Data_Currency = "USD";
        public bool Data_OnlineEvent = true;
        public bool Data_Listed = true;
        public bool Data_Shareable = true;


        public BaseEventSetup()
        {
            Config = new ConfigurationLoader().Load("appsettings.json", Directory.Bin);

            Service = new EventbriteService(ApiUrl, Token);

            Data_ExecutionStartUtc = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, 0, 0);

            var tomorrow = DateTime.UtcNow.AddDays(1); // create an event for tomorrow
            Data_StartLocal = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 17, 0, 0, 0);
            Data_EndLocal = Data_StartLocal.AddHours(1);
            ScaffoldEvent();
        }


        public virtual void Dispose()
        {
            var res = Service.DeleteEvent(Event.Id).Result;

            Assert.True(res);
        }


        public Event ScaffoldEvent()
        {
            Data_StartUtc = Data_StartLocal.ToUniversalTime(); //Data_ExecutionStartUtc.AddDays(1);
            Data_EndUtc = Data_StartUtc.AddHours(1);


            var @event = RequestModelBuilder.BuildEvent(Data_Title, Data_StartUtc, Data_EndUtc, Data_Timezone, Data_Currency);          

            if (ApiUrl.Contains("mock"))
            {
                // mock api returns incomplete objects :(
                Event = new Event()
                {
                    OrganizationId = 1234567890,
                    Start = new EventDate { Local = Data_StartLocal, Utc = Data_StartUtc, Timezone = Data_Timezone },
                    End = new EventDate { Local = Data_EndLocal, Utc = Data_EndUtc, Timezone = Data_Timezone },
                };
            }
            else
            {
                Event = Service.CreateEvent(@event).Result;
            }

            return Event;
        }
    }
}
