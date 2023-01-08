using Vila.Web.Models.Vila;
using Vila.Web.Services.Generic;

namespace Vila.Web.Services.Vila
{
    public interface IVilaRepository : IRepository<VilaModel>
    {
        Task<VilaPaging> search(int pageId, string filter, int take,string token);
    }
}
