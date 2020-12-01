# DangEasy.Eventbrite
A simple library for creating events on Eventbrite. Wraps the [Eventbrite v3 API](https://www.eventbrite.com/platform/api).

Note: This library is not yet comprehensive. It only contains API parts which I required for my personal projects. Contributions are welcome!


# Installation

Use NuGet to install the [package](https://www.nuget.org/packages/DangEasy.Eventbrite/).

```
PM> Install-Package DangEasy.Eventbrite
```


# Getting Started

## Initialize Service
```cs
var service = new EventbriteService(ApiUrl, Token);
```
Token 
- Bearer Token
- Listed as Private token on the [API Keys](https://www.eventbrite.com/account-settings/apps) page

ApiUrl
* Production - https://www.eventbriteapi.com/v3
* Mock - https://private-anon-1050bc6064-eventbriteapiv3public.apiary-mock.com/v3/
* Proxy - https://private-anon-7282f52469-eventbriteapiv3public.apiary-proxy.com/v3/

## Creating An Event
```cs
var @event = new Event
                {
                    Name = "My Event",
                    Start = new DateTime(2021, 1, 15, 13, 30, 0, 0),
                    End = new DateTime(2021, 1, 15, 16, 30, 0, 0),
                    StartTimezone = "Europe/London",
                    EndTimezone = "Europe/London",
                    Currency = "USD",
                    OnlineEvent = true,
                    Listed = true,
                    Shareable = true
                };
var myEvent = await service.CreateEvent(@event);

var structuredContent = RequestModelBuilder.BuildStructuredContentText("My event description");
_ = await service.CreateStructuredContent(myEvent.Id, structuredContent);

// API requires ticket class before publish
var ticketClass = new TicketClass
                    {
                        Name = "General Admission",
                        Capacity = 10,
                        DeliveryMethods = new List<string>() { "electronic" },
                        Free = true,
                        SalesStartUtc = DateTime.UtcNow,
                        SalesEndUtc = new DateTime(2021, 1, 15, 15, 30, 0, 0)
                    };
_ = await service.CreateTicketClass(myEvent.Id, ticketClass);

var published = await service.PublishEvent(myEvent.Id);

```


# Tests
Eventbrite has 2 testing endpoints
1. [Mock](https://private-anon-1050bc6064-eventbriteapiv3public.apiary-mock.com/v3/)  
  * This returns some incomplete json objects which causes pain while testing 
    * eg. empty string for organisationId, null for sales_start
2. [Proxy](https://private-anon-7282f52469-eventbriteapiv3public.apiary-proxy.com/v3/) 
    * This is a complete API (from what I can tell)
     
Note: Do not use the production API endpoint to do automated testing. Eventbrite detects this and will lock your account. 



