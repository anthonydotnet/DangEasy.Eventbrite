using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using RequestModels = DangEasy.Eventbrite.Models.Request;
using System.Linq;
using DangEasy.Eventbrite.Models.Response;
using DangEasy.Eventbrite.Models.Response.Paginated;
using RequestConstants = DangEasy.Eventbrite.Constants.Request;

namespace DangEasy.Eventbrite.Services
{
    public interface IEventbriteService
    {
        long OrganizationId { get; }

        Task<EventCancelled> CancelEvent(long eventId);
        Task<Event> CopyEvent(long eventId);
        Task<Event> CreateEvent(RequestModels.Event @event);
        Task<StructuredContentPaginated> CreateStructuredContent(long eventId, RequestModels.StructuredContent content);
        Task<StructuredDigitalContentPaginated> CreateStructuredDigitalContent(long eventId, RequestModels.StructuredDigitalContent content);
        Task<TicketClass> CreateTicketClass(long eventId, RequestModels.TicketClass ticketClass);

        Task<bool> DeleteEvent(long eventId);

        Task<Event> GetEvent(long eventId);
        Task<EventPaginated> GetEvents(long organizationId);
        Task<Organization> GetOrganization(long id);

        Task<StructuredContentPaginated> GetStructuredContent(long eventId);
        Task<StructuredDigitalContentPaginated> GetStructuredDigitalContent(long eventId);

        Task<TicketClassPaginated> GetTicketClasses(long eventId);
        Task<bool> PublishEvent(long eventId);
        Task<bool> UnPublishEvent(long eventId);

        Task<Event> UpdateEvent(long eventId, RequestModels.Event @event);
        Task<Event> UpdateEvent(long eventId, string property, string value);
        Task<TicketClass> UpdateTicketClass(long eventId, long ticketClassId, RequestModels.TicketClass ticketClass);
    }


    public class EventbriteService : IEventbriteService
    {
        string _baseUrl;
        const string DateFormatString = "yyyy-MM-ddTHH:mm:ssZ";
        const string ContentTypeJson = "application/json";

        readonly string _bearerToken;
        private long _organizationId;

        public long OrganizationId
        {
            get
            {
                if (_organizationId == 0)
                {
                    var org = GetOrganization(_organizationId);
                    _organizationId = org.Result.Id;
                }

                return _organizationId;
            }
        }

        public EventbriteService(string apiUrl, string bearerToken, long organizationId = 0)
        {
            _baseUrl = apiUrl;
            _bearerToken = bearerToken;
            _organizationId = organizationId;
        }



        public async Task<EventCancelled> CancelEvent(long eventId)
        {
            var request = BuildRequest($"events/{eventId}/cancel/", ContentTypeJson);

            var res = await request.PostAsync().ReceiveJson<EventCancelled>();
            return res;
        }



        public async Task<Event> CopyEvent(long eventId)
        {
            var request = BuildRequest($"events/{eventId}/copy/", ContentTypeJson);

            var res = await request.PostAsync().ReceiveJson<Event>();
            return res;
        }


        public async Task<Event> CreateEvent(RequestModels.Event @event)
        {
            var request = BuildRequest($"organizations/{OrganizationId}/events/", ContentTypeJson);

            var json = JsonConvert.SerializeObject(@event, new JsonSerializerSettings
            {
                DateFormatString = DateFormatString
            });

            var res = await request.PostStringAsync(json).ReceiveJson<Event>();

            return res;
        }



        public async Task<StructuredContentPaginated> CreateStructuredContent(long eventId, RequestModels.StructuredContent content)
        {
            var current = await GetStructuredContent(eventId);

            var request = BuildRequest($"events/{eventId}/structured_content/{current.PageVersionNumber ?? 1}/", ContentTypeJson);

            var res = await request.PostJsonAsync(content).ReceiveJson<StructuredContentPaginated>();

            return res;
        }


        public async Task<StructuredDigitalContentPaginated> CreateStructuredDigitalContent(long eventId, RequestModels.StructuredDigitalContent content)
        {
            var current = await GetStructuredDigitalContent(eventId);

            var request = BuildRequest($"events/{eventId}/structured_content/{current.PageVersionNumber ?? 1}/", ContentTypeJson);

            var json = JsonConvert.SerializeObject(content);
            var res = await request.PostStringAsync(json).ReceiveJson<StructuredDigitalContentPaginated>();

            return res;
        }


        public async Task<TicketClass> CreateTicketClass(long eventId, RequestModels.TicketClass ticketClass)
        {
            var request = BuildRequest($"events/{eventId}/ticket_classes/", ContentTypeJson);

            var json = JsonConvert.SerializeObject(ticketClass, new JsonSerializerSettings
            {
                DateFormatString = DateFormatString
            });

            var res = await request.PostStringAsync(json).ReceiveJson<TicketClass>();

            return res;
        }


        public async Task<bool> DeleteEvent(long eventId)
        {
            var request = BuildRequest($"events/{eventId}/");

            dynamic res = await request.DeleteAsync().ReceiveJson();

            return res.deleted;
        }


        public async Task<Event> GetEvent(long eventId)
        {
            var request = BuildRequest($"events/{eventId}/");

            var res = await request.GetAsync().ReceiveJson<Event>();

            return res;
        }


        public async Task<EventPaginated> GetEvents(long organizationId)
        {
            var request = BuildRequest($"organizations/{organizationId}/events/", ContentTypeJson);

            var res = await request.GetJsonAsync<EventPaginated>();

            return res;
        }

        public async Task<StructuredContentPaginated> GetStructuredContent(long eventId)
        {
            var request = BuildRequest($"events/{eventId}/structured_content/");
            request.SetQueryParam("purpose", RequestConstants.StructuredContent.Purpose);

            var res = await request.GetJsonAsync<StructuredContentPaginated>();

            return res;
        }


        public async Task<StructuredDigitalContentPaginated> GetStructuredDigitalContent(long eventId)
        {
            var request = BuildRequest($"events/{eventId}/structured_content/");
            request.SetQueryParam("purpose", RequestConstants.StructuredDigitalContent.Purpose);

            StructuredDigitalContentPaginated res;

            try
            {
                res = await request.GetJsonAsync<StructuredDigitalContentPaginated>();
                return res;
            }
            catch (FlurlHttpException ex)
            {
                // API does not behave the same when it comes to purpose = digital_content!!!!
                var error = await ex.GetResponseJsonAsync<Error>();

                if (error.StatusCode == "404")
                {
                    return new StructuredDigitalContentPaginated();
                }
                throw;
            }

        }


        public async Task<TicketClassPaginated> GetTicketClasses(long eventId)
        {
            var request = BuildRequest($"events/{eventId}/ticket_classes/");

            var res = await request.GetJsonAsync<TicketClassPaginated>();

            return res;
        }


        public async Task<bool> PublishEvent(long eventId)
        {
            var request = BuildRequest($"events/{eventId}/publish/");
            var res = await request.PostStringAsync("").ReceiveJson();

            return res.published;
        }


        public async Task<bool> UnPublishEvent(long eventId)
        {
            var request = BuildRequest($"events/{eventId}/unpublish/");
            var res = await request.PostStringAsync("").ReceiveJson();

            return res.unpublished;
        }


        public async Task<Organization> GetOrganization(long id)
        {
            var request = BuildRequest($"users/me/organizations/", ContentTypeJson);

            var res = await request.GetAsync().ReceiveJson<OrganizationPaginated>();

            return id != 0 ? res.Organizations.First(x => x.Id == id) : res.Organizations.First();
        }


        public async Task<Event> UpdateEvent(long eventId, RequestModels.Event @event)
        {
            var request = BuildRequest($"events/{eventId}/", ContentTypeJson);

            var json = JsonConvert.SerializeObject(@event, new JsonSerializerSettings
            {
                DateFormatString = DateFormatString
            });

            var res = await request.PostStringAsync(json).ReceiveJson<Event>();

            return res;
        }


        // usage eg... property: "event.logo_id", value "1234567890"
        public async Task<Event> UpdateEvent(long eventId, string property, string value)
        {
            var request = BuildRequest($"events/{eventId}/", ContentTypeJson);

            // this is a bit of a hack :(
            var json = $"\"{property}\": \"{value}\"";
            json = "{" + json + "}";

            var res = await request.PostStringAsync(json).ReceiveJson<Event>();

            return res;
        }


        public async Task<TicketClass> UpdateTicketClass(long eventId, long ticketClassId, RequestModels.TicketClass ticketClass)
        {
            var request = BuildRequest($"events/{eventId}/ticket_classes/{ticketClassId}/", ContentTypeJson);

            var json = JsonConvert.SerializeObject(ticketClass, new JsonSerializerSettings
            {
                DateFormatString = DateFormatString
            });

            var res = await request.PostStringAsync(json).ReceiveJson<TicketClass>();

            return res;
        }


        #region private
        private FlurlRequest BuildRequest(string path, string contentType = null)
        {
            var request = new FlurlRequest().WithOAuthBearerToken(_bearerToken);
            request.Url = new Url($"{_baseUrl}{path}");

            if (!string.IsNullOrWhiteSpace(contentType))
            {
                request.Headers.Add("Content-Type", contentType);
            }

            return request;
        }

       



        #endregion
    }
}
