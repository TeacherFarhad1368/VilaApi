using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using Vila.Web.Models;
using Vila.Web.Models.Customer;
using Vila.Web.Utility;

namespace Vila.Web.Services.Customer
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApiUrls _urls;
        private readonly IHttpClientFactory _client;
        public CustomerRepository(IOptions<ApiUrls> urls, IHttpClientFactory client)
        {
            _urls = urls.Value;
            _client = client;
        }

        public async Task<LoginResultModel> Login(RegisterModel model)
        {
            var url = $"{_urls.BaseAddress}{_urls.CustomerAddress}/Login";

            var request = new HttpRequestMessage(HttpMethod.Post, url);


            request.Content = new
                StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var myClient = _client.CreateClient();

            HttpResponseMessage responseMessage = await myClient.SendAsync(request);


            OperationResult operationResult = new();
            CustomerModel customer = new();

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                customer = JsonConvert.DeserializeObject<CustomerModel>(jsonString);

                operationResult.Result = true;
                operationResult.Message = "ورود با موفقیت انجام شد .";
            }
            else if(responseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
               var modelError = JsonConvert.DeserializeObject<ErrorViewModel>(jsonString);

                customer = null;
                operationResult.Result = false;
                operationResult.Message = modelError.Error;
            }
            else
            {
                customer = null;
                operationResult.Result = false;
                operationResult.Message = "خطای سمت سرور";
            }
            return new()
            {
                Customer=customer,
                Result=operationResult
            };
        }

        public async Task<OperationResult> Register(RegisterModel model)
        {
            var url = $"{_urls.BaseAddress}{_urls.CustomerAddress}/Register";
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Content = new
                StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var myClient = _client.CreateClient();

            HttpResponseMessage responseMessage = await myClient.SendAsync(request);

            OperationResult operationResult = new();

            if(responseMessage.StatusCode == System.Net.HttpStatusCode.Created)
            {
                operationResult.Result = true;
                operationResult.Message = "";
            }
            else if(responseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ErrorViewModel>(jsonString);
                operationResult.Result = false;
                operationResult.Message = res.Error;
            }
            else
            {
                operationResult.Result = false;
                operationResult.Message = "خطای سمت سرور .....";
            }
            return operationResult;
        }
    }
}
