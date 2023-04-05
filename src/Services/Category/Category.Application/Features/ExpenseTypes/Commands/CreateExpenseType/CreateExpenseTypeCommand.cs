using Core.Service.Commands;

namespace Category.Application.Features.ExpenseTypes.Commands.CreateExpenseType
{
    public class CreateExpenseTypeCommand : Command<Guid?>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public CreateExpenseTypeCommand(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public override bool IsValid() => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Description);

    }
}
