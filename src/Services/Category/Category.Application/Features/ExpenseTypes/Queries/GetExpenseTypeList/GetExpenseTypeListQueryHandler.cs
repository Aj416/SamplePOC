using AutoMapper;
using Category.Application.Contracts.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Category.Application.Features.ExpenseTypes.Queries.GetExpenseTypeList
{
    public class GetExpenseTypeListQueryHandler : IRequestHandler<GetExpenseTypeListQuery, IList<ExpenseTypeModel>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public GetExpenseTypeListQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<IList<ExpenseTypeModel>> Handle(GetExpenseTypeListQuery request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.GetAll().ToListAsync();
            return _mapper.Map<IList<ExpenseTypeModel>>(result);
        }
    }
}