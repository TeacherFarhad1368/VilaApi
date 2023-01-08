namespace Vila.Web.Models.Vila
{
    public class VilaPaging
    {
        public int DataCount { get; set; }
        public int PageId { get; set; }
        public int PageCount { get; set; }
        public int Take { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public List<VilaSearchModel> Vilas { get; set; }
        public string Filter { get; set; }
    }
}
