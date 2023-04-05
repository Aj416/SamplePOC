using Category.Application.Contracts.Persistence;
using Core.Service.Bus;
using Core.Service.Commands;
using Core.Service.Notifications;
using Core.Service.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Category.Application.Features.ExpenseTypes.Commands.DeleteExpenseType
{
    public class DeleteExpenseTypeCommandHandler : CommandHandler, IRequestHandler<DeleteExpenseTypeCommand, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<DeleteExpenseTypeCommandHandler> _logger;

        public DeleteExpenseTypeCommandHandler(ICategoryRepository categoryRepository, ILogger<DeleteExpenseTypeCommandHandler> logger, IUnitOfWork uow, IMediatorHandler mediator, IDomainNotificationHandler notifications) : base(mediator, uow, notifications)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteExpenseTypeCommand request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.FindAsync(request.Id);
            if (result == null)
            {
                _logger.LogError($"Expense type with id {request.Id} not found");
                await _mediator.RaiseEvent(new DomainNotification(request.MessageType, $"Expense type with id {request.Id} not found", 404));
                return Unit.Value;
            }

            _categoryRepository.Delete(result);

            if (await Commit())
            {
                return Unit.Value;
            }
            else
            {
                _logger.LogError($"Not able to delete expense type with id {request.Id}");
                await _mediator.RaiseEvent(new DomainNotification(request.MessageType, $"Not able to delete expense type with id {request.Id}", 400));
                return Unit.Value;
            }
        }
    }
}
