using AutoMapper;
using Category.Application.Contracts.Persistence;
using Category.Application.Features.ExpenseTypes.Events.ExpenseTypeCreated;
using Category.Application.Features.ExpenseTypes.Events.ExpenseTypeDeleted;
using Category.Application.Features.ExpenseTypes.Events.ExpenseTypeUpdated;
using Category.Application.Models.Search;
using MediatR;
using Search.Service.Interfaces;

namespace Category.Application.Features.ExpenseTypes.Events
{
    public class ExpenseTypeEventHandler : INotificationHandler<ExpenseTypeCreatedEvent>,
        INotificationHandler<ExpenseTypeUpdatedEvent>,
        INotificationHandler<ExpenseTypeDeletedEvent>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ISearchService<CategorySearchModel> _searchService;

        public ExpenseTypeEventHandler(ICategoryRepository categoryRepository, IMapper mapper, ISearchService<CategorySearchModel> searchService)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _searchService = searchService;
        }

        public async Task Handle(ExpenseTypeCreatedEvent notification, CancellationToken cancellationToken)
        => await GetAndIndexCategory(notification.Id);

        public async Task Handle(ExpenseTypeUpdatedEvent notification, CancellationToken cancellationToken)
        => await GetAndIndexCategory(notification.Id);

        public async Task Handle(ExpenseTypeDeletedEvent notification, CancellationToken cancellationToken)
        => await _searchService.Delete(notification.Id);

        private async Task GetAndIndexCategory(Guid id)
        {
            var model = await _categoryRepository.GetFirstOrDefaultAsync<CategorySearchModel>(_mapper, x => x.Id == id);

            await _searchService.Index(model);
        }
    }
}
