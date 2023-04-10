using Category.API.Models;
using Category.Application.Features.ExpenseTypes.Commands.CreateExpenseType;
using Category.Application.Features.ExpenseTypes.Commands.DeleteExpenseType;
using Category.Application.Features.ExpenseTypes.Commands.UpdateExpenseType;
using Category.Application.Features.ExpenseTypes.Queries.GetExpenseType;
using Category.Application.Features.ExpenseTypes.Queries.GetExpenseTypeList;
using Category.Application.Models.Search;
using Core.Service.Bus;
using Core.Service.Controllers;
using Core.Service.Notifications;
using Microsoft.AspNetCore.Mvc;
using Search.Service.Interfaces;

namespace Category.API.Controllers
{
    [Route("api/[controller]")]

    public class CategoryController : ApiController
    {
        private readonly ISearchService<CategorySearchModel> _categorySearchService;

        public CategoryController(IMediatorHandler mediatorHandler, IDomainNotificationHandler domainNotificationHandler, ISearchService<CategorySearchModel> categorySearchService) : base(domainNotificationHandler, mediatorHandler)
        {
            _categorySearchService = categorySearchService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _mediator.SendQuery(new GetExpenseTypeListQuery());

            return result != null ? Response(result) : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var result = await _mediator.SendQuery(new GetExpenseTypeQuery(id));
            return Response(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryModel category)
        {
            await _mediator.SendCommand(new CreateExpenseTypeCommand(category.Name, category.Description));
            return Response(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryModel category)
        {
            await _mediator.SendCommand(new UpdateExpenseTypeCommand(id, category.Name, category.Description));
            return Response();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _mediator.SendCommand(new DeleteExpenseTypeCommand(id));
            return Response();
        }

        [HttpGet("search")]
        public async Task<IActionResult> CategorySearch([FromQuery] CategorySearchCriteriaModel model)
        {
            return Response(await _categorySearchService.Search(model));
        }
    }
}
