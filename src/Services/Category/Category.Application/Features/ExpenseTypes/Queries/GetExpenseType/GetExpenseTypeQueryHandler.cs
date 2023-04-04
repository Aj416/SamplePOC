using AutoMapper;
using Category.Application.Contracts.Persistence;
using MediatR;

namespace Category.Application.Features.ExpenseTypes.Queries.GetExpenseType
{
    public class GetExpenseTypeQueryHandler : IRequestHandler<GetExpenseTypeQuery, ExpenseTypeModel>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public GetExpenseTypeQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<ExpenseTypeModel> Handle(GetExpenseTypeQuery request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.FindAsync(request.Id);
            return _mapper.Map<ExpenseTypeModel>(result);
        }
    }
}
