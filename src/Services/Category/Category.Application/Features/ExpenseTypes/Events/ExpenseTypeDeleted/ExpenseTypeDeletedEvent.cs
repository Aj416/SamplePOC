using Core.Service.Events;

namespace Category.Application.Features.ExpenseTypes.Events.ExpenseTypeDeleted
{
    public class ExpenseTypeDeletedEvent : Event
    {
        public Guid Id { get; set; }

        public ExpenseTypeDeletedEvent(Guid id) => Id = id;
    }
}
