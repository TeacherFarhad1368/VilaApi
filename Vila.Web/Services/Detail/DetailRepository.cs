using Vila.Web.Models.Detail;
using Vila.Web.Services.Generic;

namespace Vila.Web.Services.Detail
{
    public class DetailRepository : Repository<DetailModel>, IDetailRepository
    {
        private readonly IHttpClientFactory _client;
        public DetailRepository(IHttpClientFactory client) : base(client)
        {
            _client = client;
        }
    }
}
