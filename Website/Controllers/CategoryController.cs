using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Website.Application.Commands.CategoryCommands;
using Website.Application.Queries;
using Website.Domain.Entities;
using Website.Infrastructure.IRepositories;
using Website.Models;

namespace Website.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IMediator _mediator;
     

        public CategoryController(ILogger<CategoryController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IActionResult Index()
        {
           
            return View();
        }

        public async Task<IActionResult> Get(int id)
        {
            var command = new GetCategoryByIdQuery { Id = id };
            var res =await _mediator.Send(command);
            return new EmptyResult();
        }

        public async Task<IActionResult> Create()
        {
            var str = Guid.NewGuid().ToString();
            var command = new CreateCategoryCommand { Name = "Cate " + str, Description ="Des " + str };
            var res1 = await _mediator.Send(command);
            return new EmptyResult();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
