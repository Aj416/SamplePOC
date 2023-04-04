using Core.Service.Queries;

namespace Category.Application.Features.ExpenseTypes.Queries.GetExpenseTypeList
{
    public class GetExpenseTypeListQuery : Query<IList<ExpenseTypeModel>>
    {
        public override bool IsValid() => true;
    }
}
