using Core.Service.Commands;
using MediatR;

namespace Category.Application.Features.ExpenseTypes.Commands.DeleteExpenseType
{
    public class DeleteExpenseTypeCommand : Command<Unit>
    {
        public Guid Id { get; set; }

        public DeleteExpenseTypeCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid() => Id != Guid.Empty;
    }
}
