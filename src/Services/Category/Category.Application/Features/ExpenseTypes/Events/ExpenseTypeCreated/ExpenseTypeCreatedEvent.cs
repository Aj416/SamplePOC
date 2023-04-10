using Core.Service.Events;

namespace Category.Application.Features.ExpenseTypes.Events.ExpenseTypeCreated
{
    public class ExpenseTypeCreatedEvent : Event
    {
        public Guid Id { get; set; }

        public ExpenseTypeCreatedEvent(Guid id) => Id = id;
    }
}
