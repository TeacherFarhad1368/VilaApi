using Vila.WebApi.Paging;

namespace Vila.WebApi.Services.Vila
{
    public interface IVilaService
    {
        List<Models.Vila> GetAll();
        Models.Vila GetById(int id);
        bool Create(Models.Vila model);
        bool Update(Models.Vila model);
        bool Delete(Models.Vila model);
        bool Save();
        VilaPaging SearchVila(int pageId, string filter, int take);
        VilaAdminPaging SearchVilaAdmin(int pageId, string filter, int take);
    }
}
