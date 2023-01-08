using Vila.WebApi.Dtos;

namespace Vila.WebApi.Paging
{
    public class VilaAdminPaging :BasePaging
    {
        public List<VilaDto> VilaDtos { get; set; }
        public string Filter { get; set; }
    }
}
