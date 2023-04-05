using Core.Service.Queries;

namespace Category.Application.Features.ExpenseTypes.Queries.GetExpenseType
{
    public class GetExpenseTypeQuery : Query<ExpenseTypeModel>
    {
        public Guid Id { get; set; }

        public GetExpenseTypeQuery(Guid id)
        {
            Id = id;
        }
        public override bool IsValid() => Id != Guid.Empty;

    }
}
