namespace Vila.WebApi.Services.Detail
{
    public interface IDetailService
    {
        List<Models.Detail> GetAllVilaDetails(int vilaId);
        Models.Detail GetById(int id);
        bool Create(Models.Detail model);
        bool Update(Models.Detail model);
        bool Delete(Models.Detail model);
        bool Save();
    }
}
