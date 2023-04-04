namespace Core.Service.Models.PagedList
{
    public class PageRequestModel
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 25;
    }
}
