using System;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using RequestModels = DangEasy.Eventbrite.Models.Request;
using ResponseModels = DangEasy.Eventbrite.Models.Response;
using System.Linq;

namespace DangEasy.Eventbrite.Services
{
    public interface IEventbriteService
    {
        Task<ResponseModels.StructuredContent> CreateStructuredContent(long eventId, RequestModels.StructuredContent content);
        Task<ResponseModels.StructuredContent> CreateStructuredDigitalContent(long eventId, RequestModels.StructuredDigitalContent content);
        Task<ResponseModels.Event> CreateEvent(RequestModels.Event @event);
        Task<ResponseModels.TicketClass> CreateTicketClass(long eventId, RequestModels.TicketClass ticketClass);

        Task<bool> DeleteEvent(long eventId);

        Task<ResponseModels.Event> DuplicateEvent(long eventId);
        Task<ResponseModels.Event> GetEvent(long eventId);
        Task<ResponseModels.Organization> GetOrganization(long id);

        Task<ResponseModels.StructuredContent> GetStructuredContent(long eventId, string purpose);
        Task<ResponseModels.StructuredDigitalContent> GetStructuredDigitalContent(long eventId, string purpose);

        Task<ResponseModels.Paginated.TicketClassPaginated> GetTicketClasses(long eventId);
        Task<bool> PublishEvent(long eventId);
        Task<bool> UnPublishEvent(long eventId);

        Task<ResponseModels.Event> UpdateEvent(long eventId, RequestModels.Event @event);
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


        public async Task<ResponseModels.TicketClass> CreateTicketClass(long eventId, RequestModels.TicketClass ticketClass)
        {
            var request = BuildRequest($"events/{eventId}/ticket_classes/", ContentTypeJson);

            var json = JsonConvert.SerializeObject(ticketClass, new JsonSerializerSettings
            {
                DateFormatString = DateFormatString
            });

            var res = await request.PostStringAsync(json).ReceiveJson<ResponseModels.TicketClass>();
           
            return res;
        }


        public async Task<ResponseModels.StructuredContent> CreateStructuredContent(long eventId, RequestModels.StructuredContent content)
        {
            var request = BuildRequest($"events/{eventId}/structured_content/", ContentTypeJson);
           
            var res = await request.PostJsonAsync(content).ReceiveJson<ResponseModels.StructuredContent>();

            return res;
        }


        public async Task<ResponseModels.StructuredContent> CreateStructuredDigitalContent(long eventId, RequestModels.StructuredDigitalContent content)
        {
            var request = BuildRequest($"events/{eventId}/structured_content/", ContentTypeJson);

            var json = JsonConvert.SerializeObject(content);
            var res = await request.PostStringAsync(json).ReceiveJson<ResponseModels.StructuredContent>();

            return res;
        }



        public async Task<ResponseModels.Event> CreateEvent(RequestModels.Event @event)
        {
            var request = BuildRequest($"organizations/{OrganizationId}/events/", ContentTypeJson);

            var json = JsonConvert.SerializeObject(@event, new JsonSerializerSettings
            {
                DateFormatString = DateFormatString
            });

            var res = await request.PostStringAsync(json).ReceiveJson<ResponseModels.Event>();

            return res;
        }


        public async Task<bool> DeleteEvent(long eventId)
        {
            var request = BuildRequest($"events/{eventId}/");

            dynamic res = await request.DeleteAsync().ReceiveJson();

            return res.deleted;
        }


        public async Task<ResponseModels.Event> DuplicateEvent(long eventId)
        {
            var request = BuildRequest($"{eventId}/copy/", ContentTypeJson);

            //var res = await request.PostStringAsync("").ReceiveJson();
            //return long.Parse(res.id);

            var res = await request.GetAsync().ReceiveJson<ResponseModels.Event>();
            return res;
        }


        public async Task<ResponseModels.Event> GetEvent(long eventId)
        {
            var request = BuildRequest($"events/{eventId}/");

            var res = await request.GetAsync().ReceiveJson<ResponseModels.Event>();

            return res;
        }


        public async Task<ResponseModels.StructuredContent> GetStructuredContent(long eventId, string purpose)
        {
            var request = BuildRequest($"events/{eventId}/structured_content/");
            request.SetQueryParam("purpose", purpose);

            var res = await request.GetJsonAsync<ResponseModels.StructuredContent>();

            return res;
        }


        public async Task<ResponseModels.StructuredDigitalContent> GetStructuredDigitalContent(long eventId, string purpose)
        {
            var request = BuildRequest($"events/{eventId}/structured_content/");
            request.SetQueryParam("purpose", purpose);

            ResponseModels.StructuredDigitalContent res;

            try
            {
                res = await request.GetJsonAsync<ResponseModels.StructuredDigitalContent>();
                return res;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("404"))
                {
                    return new ResponseModels.StructuredDigitalContent();
                }
                else
                {
                    throw;
                }
            }

        }


        public async Task<ResponseModels.Paginated.TicketClassPaginated> GetTicketClasses(long eventId)
        {
            var request = BuildRequest($"events/{eventId}/ticket_classes/");

            var res = await request.GetJsonAsync<ResponseModels.Paginated.TicketClassPaginated>();

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


        public async Task<ResponseModels.Organization> GetOrganization(long id)
        {
            var request = BuildRequest($"users/me/organizations/", ContentTypeJson);

            var res = await request.GetAsync().ReceiveJson<ResponseModels.Paginated.OrganizationPaginated>();

            return id != 0 ? res.Organizations.First(x => x.Id == id) : res.Organizations.First();
        }


        public async Task<ResponseModels.Event> UpdateEvent(long eventId, RequestModels.Event @event)
        {
            var request = BuildRequest($"events/{eventId}/", ContentTypeJson);

            var json = JsonConvert.SerializeObject(@event, new JsonSerializerSettings
            {
                DateFormatString = DateFormatString
            });

            var res = await request.PostStringAsync(json).ReceiveJson<ResponseModels.Event>();

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
