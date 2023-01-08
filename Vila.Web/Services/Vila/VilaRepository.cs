using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Vila.Web.Models.Vila;
using Vila.Web.Services.Generic;
using Vila.Web.Utility;

namespace Vila.Web.Services.Vila
{
    public class VilaRepository :Repository<VilaModel> ,  IVilaRepository
    {
        private readonly ApiUrls _urls;
        private readonly IHttpClientFactory _client;
        public VilaRepository(IOptions<ApiUrls> urls, IHttpClientFactory client) : base(client)
        {
            _urls = urls.Value;
            _client = client;
        }
        public async Task<VilaPaging> search(int pageId, string filter, int take, string token)
        {
            var url = $"{_urls.BaseAddress}{_urls.VilaV2Address}?pageId={pageId}&filter={filter}&take={take}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var myClient = _client.CreateClient();

            //myClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            myClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");


            // send version api
            //myClient.DefaultRequestHeaders.Add("X-ApiVersion", "2");


            HttpResponseMessage responseMessage = await myClient.SendAsync(request);



            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                var paging = JsonConvert.DeserializeObject<VilaPaging>(jsonString);
                return paging;
            }
            return null;
        }
    }
}
