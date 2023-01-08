using Vila.WebApi.Context;
using Vila.WebApi.Models;

namespace Vila.WebApi.Services.Detail
{
    public class DetailService : IDetailService
    {
        private readonly DataContext _context;
        public DetailService(DataContext context)
        {
            _context = context;
        }
        public bool Create(Models.Detail model)
        {
            _context.Detail.Add(model);
            return Save();
        }

        public bool Delete(Models.Detail model)
        {
            _context.Detail.Remove(model);
            return Save();
        }

        public List<Models.Detail> GetAllVilaDetails(int vilaId) =>
            _context.Detail.Where(D=>D.VilaId == vilaId).ToList();

        public Models.Detail GetById(int id) =>
            _context.Detail.FirstOrDefault(v => v.DetailId == id);

        public bool Save() =>
            _context.SaveChanges() >= 0 ? true : false;

        public bool Update(Models.Detail model)
        {
            _context.Detail.Update(model);
            return Save();
        }
    }
}
