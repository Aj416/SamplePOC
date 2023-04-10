using Core.Service.Events;

namespace Category.Application.Features.ExpenseTypes.Events.ExpenseTypeUpdated
{
    public class ExpenseTypeUpdatedEvent : Event
    {
        public Guid Id { get; set; }
        public ExpenseTypeUpdatedEvent(Guid id) => Id = id;
    }
}
