using Newtonsoft.Json;
using System.Text;

namespace Vila.Web.Services.Generic
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory _client;
        public Repository(IHttpClientFactory client)
        {
            _client = client;
        }
        public async Task<bool> Create(string url, string token, T model)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Content = new StringContent
                (JsonConvert.SerializeObject(model),Encoding.UTF8, "application/json");

            var myClient = _client.CreateClient();

            myClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            HttpResponseMessage responseMessage = await myClient.SendAsync(request);

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.Created)
                return true;

            return false;
        }

        public async Task<bool> Delete(string url, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url);

            var myClient = _client.CreateClient();

            myClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            HttpResponseMessage responseMessage = await myClient.SendAsync(request);

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                return true;

            return false;
        }

        public async Task<List<T>> GetAll(string url, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var myClient = _client.CreateClient();

            myClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            HttpResponseMessage responseMessage = await myClient.SendAsync(request);

            if(responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<List<T>>(json);
                return res;
            }
            return null;
        }

        public async Task<T> GetById(string url, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var myClient = _client.CreateClient();

            myClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            HttpResponseMessage responseMessage = await myClient.SendAsync(request);

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<T>(json);
                return res;
            }
            return null;
        }

        public async Task<bool> Update(string url, string token, T model)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, url);

            request.Content = new StringContent
                (JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var myClient = _client.CreateClient();

            myClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            HttpResponseMessage responseMessage = await myClient.SendAsync(request);

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                return true;

            return false;
        }
    }
}
