using AutoMapper;
using Category.Application.Contracts.Persistence;
using Core.Service.Bus;
using Core.Service.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Category.Application.Features.ExpenseTypes.Queries.GetExpenseType
{
    public class GetExpenseTypeQueryHandler : IRequestHandler<GetExpenseTypeQuery, ExpenseTypeModel>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<GetExpenseTypeQueryHandler> _logger;
        private readonly IMediatorHandler _mediator;

        public GetExpenseTypeQueryHandler(IMapper mapper, ICategoryRepository categoryRepository, ILogger<GetExpenseTypeQueryHandler> logger, IMediatorHandler mediator)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ExpenseTypeModel> Handle(GetExpenseTypeQuery request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.FindAsync(request.Id);

            if (result == null)
            {
                _logger.LogError("GetExpenseTypeQueryHandler - Category with id {0} not found", request);
                await _mediator.RaiseEvent(new DomainNotification(request.MessageType, $"Category with id {request.Id} not found", 404));
            }
            return _mapper.Map<ExpenseTypeModel>(result);
        }
    }
}
