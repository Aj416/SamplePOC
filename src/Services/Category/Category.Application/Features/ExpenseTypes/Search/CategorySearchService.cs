using AutoMapper;
using Category.Application.Contracts.Persistence;
using Category.Application.Models.Search;
using Core.Service.Extensions;
using Core.Service.Models.PagedList;
using Core.Service.Models.Search;
using Elasticsearch.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nest;
using Search.Service.Extensions;
using Search.Service.Implementations;
using Search.Service.Models;

namespace Category.Application.Features.ExpenseTypes.Search
{
    public class CategorySearchService : ElasticSearchService<CategorySearchModel>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        protected override string LiveAlias => "categories";

        public CategorySearchService(IOptions<ElasticSearchOptions> options, ILogger<ElasticSearchService<CategorySearchModel>> logger, IMapper mapper, ICategoryRepository categoryRepository) : base(options, logger)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        private string GetSortingField(CategorySortBy sortBy)
        {
            switch (sortBy)
            {
                case CategorySortBy.name:
                    return nameof(CategorySearchModel.NameSortToken);
                default:
                    return sortBy.ToString();
            }
        }

        public override async Task<IPagedList> Search(SearchBaseCriteriaModel searchCriteria)
        {
            var criteria = (CategorySearchCriteriaModel)searchCriteria;

            var result = await Client.SearchAsync<CategorySearchModel>(s =>
            {
                s.Index(LiveAlias);
                s.Query(q =>
                {
                    var qc = q.MultiMatch(
                        m =>
                            m.Fields(f =>
                            {
                                var fd = f.Fields(
                                        p => p.Id,
                                        p => p.Name,
                                        p => p.Description
                                    );

                                return fd;
                            })
                                .Operator(Operator.And)
                                .Query(criteria.Term)
                                .Type(TextQueryType.BestFields)
                                .Lenient()
                    );

                    return qc;
                })
                    .Size(criteria.PageSize)
                    .Skip(criteria.PageIndex * criteria.PageSize);
                if (criteria.SortBy != CategorySortBy.unspecified)
                {
                    s.Sort(
                        sr =>
                            sr.Field(p =>
                            {
                                var field = new Field(
                                    GetSortingField(criteria.SortBy).ToCamelCase()
                                ); //Create the filed to be sorted by
                                p.Field(field).Order((Nest.SortOrder)(int)criteria.SortOrder); // Set the sort order
                                p.Missing(-1); //When the values are null we treat them as a number to be sorted properly
                                return p;
                            })
                    );
                }
                return s;
            });

            return PagedList.FromExisting(
                result.Documents.Select(_mapper.Map<CategorySearchResponseModel>).ToList(),
                criteria.PageIndex,
                criteria.PageSize,
                result.Total,
                0
            );
        }

        public override async Task Update(Guid id, object partialTicket)
        {
            await Client.UpdateAsync<CategorySearchModel, object>(
                DocumentPath<CategorySearchModel>.Id(id),
                u =>
                    u.Index(LiveAlias)
                        .Doc(partialTicket)
                        .Upsert(new CategorySearchModel { Id = id })
                        .Refresh(Refresh.True)
                        .RetryOnConflict(10)
            );
        }

        protected override async Task<long> FillIndex(string indexName)
        {
            var page = PagedList.ForLoop<CategorySearchModel>();

            do
            {
                page = await _categoryRepository.GetPagedListAsync<CategorySearchModel>(_mapper, pageIndex: page.PageIndex + 1, orderBy: t => t.OrderBy(x => x.CreatedDate));
                await Index(indexName, page.Items.ToArray());

            } while (page.HasNextPage);

            return page.TotalCount;
        }

        protected override Func<CreateIndexDescriptor, ICreateIndexRequest> IndexDescriptor() =>
            (
                x =>
                    x.Aliases(a => a.Alias(LiveAlias))
                        .Mappings(
                            m =>
                                m.Map<CategorySearchModel>(
                                    tm =>
                                        tm.AutoMap()
                                            .Properties(
                                                p =>
                                                    p.Text(k => k.Name(nm => nm.Id).Partial())
                                                        .Text(k => k.Name(nm => nm.Name).Partial())
                                                        .Text(
                                                            k =>
                                                                k.Name(nm => nm.Description)
                                                                    .Partial()
                                                        )
                                                        .Keyword(
                                                            kw => kw.Name(nm => nm.NameSortToken)
                                                        )
                                            )
                                )
                        )
                        .Settings(s => s.PartialSearchAnalyzer())
            );
    }
}
