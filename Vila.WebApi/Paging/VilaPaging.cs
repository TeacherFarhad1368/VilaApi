using Vila.WebApi.Dtos;

namespace Vila.WebApi.Paging
{
    public class VilaPaging : BasePaging
    {
        public List<VilaSearchDto> Vilas { get; set; }
        public string Filter { get; set; }
    }
}
