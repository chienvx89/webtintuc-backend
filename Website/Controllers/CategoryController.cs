using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Website.Application.Commands.CategoryCommands;
using Website.Application.Queries;
using Website.Domain.Entities;
using Website.Infrastructure.IRepositories;
using Website.Infrastructure.Logs;
using Website.Models;

namespace Website.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;


        public CategoryController(IMediator mediator, ILoggerManager loggerManager,IMapper mapper)
        {
            _mediator = mediator;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            _loggerManager.LogDebug("ádfasdfhj");
            return new EmptyResult();
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
