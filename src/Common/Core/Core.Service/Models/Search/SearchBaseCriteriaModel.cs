using Core.Service.Models.PagedList;

namespace Core.Service.Models.Search
{
    public class SearchBaseCriteriaModel : PageRequestModel
    {
        public string Term { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public SortOrder SortOrder { get; set; } = SortOrder.Desc;
    }

    public enum SortOrder
    {
        Asc,
        Desc
    }

}
