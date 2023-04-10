using AutoMapper;
using Category.Application.Features.ExpenseTypes.Commands.UpdateExpenseType;
using Category.Application.Features.ExpenseTypes.Queries;
using Category.Application.Models.Search;
using Category.Domain.Entity;

namespace Category.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ExpenseType, ExpenseTypeModel>().ReverseMap();
            CreateMap<UpdateExpenseTypeCommand, ExpenseType>();
            CreateMap<ExpenseType, CategorySearchModel>();
            CreateMap<CategorySearchModel, CategorySearchResponseModel>();
        }
    }
}
