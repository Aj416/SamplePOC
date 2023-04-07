using AutoMapper;
using Category.Application.Contracts.Persistence;
using Core.Service.Bus;
using Core.Service.Commands;
using Core.Service.Notifications;
using Core.Service.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Category.Application.Features.ExpenseTypes.Commands.UpdateExpenseType
{
    public class UpdateExpenseTypeCommandHandler : CommandHandler, IRequestHandler<UpdateExpenseTypeCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateExpenseTypeCommandHandler> _logger;
        private readonly ICategoryRepository _categoryRepository;

        public UpdateExpenseTypeCommandHandler(IMapper mapper, ILogger<UpdateExpenseTypeCommandHandler> logger, ICategoryRepository categoryRepository, IUnitOfWork uow, IMediatorHandler mediator, IDomainNotificationHandler notifications) : base(mediator, uow, notifications)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateExpenseTypeCommand request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.FindAsync(request.Id);

            if (result == null)
            {
                _logger.LogError($"Expense Type with id {request.Id} not found");
                await _mediator.RaiseEvent(new DomainNotification(request.MessageType, $"Expense Type with id {request.Id} not found", 404));
                return Unit.Value;
            }

            result.SetExpense(request.Name, request.Description);


            _categoryRepository.Update(result);

            if (await Commit())
            {
                return Unit.Value;
            }
            else
            {
                _logger.LogError("Not able to update expense details", request);
                await _mediator.RaiseEvent(new DomainNotification(request.MessageType, "Not able to update expense details", 400));
                return Unit.Value;
            }
        }
    }
}
