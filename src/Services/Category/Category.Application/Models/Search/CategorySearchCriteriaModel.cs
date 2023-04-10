using Core.Service.Models.Search;

namespace Category.Application.Models.Search
{
    public class CategorySearchCriteriaModel : SearchBaseCriteriaModel
    {
        public CategorySortBy SortBy { get; set; }
    }

    public enum CategorySortBy
    {
        unspecified,
        name,
    }
}
