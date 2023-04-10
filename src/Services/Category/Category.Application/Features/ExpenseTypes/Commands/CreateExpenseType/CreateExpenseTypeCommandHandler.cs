using Category.Application.Contracts.Persistence;
using Category.Application.Features.ExpenseTypes.Events.ExpenseTypeCreated;
using Category.Domain.Entity;
using Core.Service.Bus;
using Core.Service.Commands;
using Core.Service.Notifications;
using Core.Service.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Category.Application.Features.ExpenseTypes.Commands.CreateExpenseType
{
    public class CreateExpenseTypeCommandHandler : CommandHandler,
        IRequestHandler<CreateExpenseTypeCommand, Guid?>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CreateExpenseTypeCommandHandler> _logger;

        public CreateExpenseTypeCommandHandler(ICategoryRepository categoryRepository, ILogger<CreateExpenseTypeCommandHandler> logger, IUnitOfWork uow, IMediatorHandler mediator, IDomainNotificationHandler notifications) : base(mediator, uow, notifications)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }
        public async Task<Guid?> Handle(CreateExpenseTypeCommand request, CancellationToken cancellationToken)
        {
            var data = new ExpenseType(request.Name, request.Description);

            await _categoryRepository.InsertAsync(data);

            if (await Commit())
            {
                await _mediator.RaiseEvent(new ExpenseTypeCreatedEvent(data.Id));
                return data.Id;
            }
            else
            {
                _logger.LogError("CreateExpenseTypeCommand - Failed to create category", request);
                await _mediator.RaiseEvent(new DomainNotification(request.MessageType, "Failed to create category", 400));
                return null;
            }
        }
    }
}
