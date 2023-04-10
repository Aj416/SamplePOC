using Core.Service.Models.PagedList;
using Core.Service.Models.Search;
using Search.Service.Models;

namespace Search.Service.Interfaces
{
    public interface ISearchService<T> where T : class
    {
        Task<IPagedList> Search(SearchBaseCriteriaModel searchCriteria);

        Task Index(T item);

        /// <summary>
        /// Pass an anonymous object with only the fields that needs to be updated.
        /// </summary>
        Task Update(Guid id, object partialItem);

        Task Delete(Guid id);
        Task<RebuildResult> Rebuild();
    }
}
