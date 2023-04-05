using Core.Service.Commands;
using MediatR;

namespace Category.Application.Features.ExpenseTypes.Commands.UpdateExpenseType
{
    public class UpdateExpenseTypeCommand : Command<Unit, UpdateExpenseTypeCommandValidator>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public UpdateExpenseTypeCommand(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
