using AutoMapper;
using Category.Application.Features.ExpenseTypes.Queries;
using Category.Domain.Entity;

namespace Category.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ExpenseType, ExpenseTypeModel>().ReverseMap();
        }
    }
}
