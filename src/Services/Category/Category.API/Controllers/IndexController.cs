using Category.Application.Models.Search;
using Core.Service.Bus;
using Core.Service.Controllers;
using Core.Service.Notifications;
using Microsoft.AspNetCore.Mvc;
using Search.Service.Interfaces;

namespace Category.API.Controllers
{
    [Route("api/[controller]")]
    public class IndexController : ApiController
    {
        private readonly ISearchService<CategorySearchModel> _categorySearchService;

        public IndexController(IMediatorHandler mediatorHandler, IDomainNotificationHandler domainNotificationHandler, ISearchService<CategorySearchModel> categorySearchService) : base(domainNotificationHandler, mediatorHandler)
        {
            _categorySearchService = categorySearchService;
        }

        [HttpGet]
        public async Task<IActionResult> RebuildIndex()
        {
            var result = await _categorySearchService.Rebuild();

            return result != null ? Response(result) : NotFound();
        }
    }
}
