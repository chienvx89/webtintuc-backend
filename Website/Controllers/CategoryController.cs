using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Website.Application.Commands.CategoryCommands;
using Website.Application.Queries;
using Website.Domain.Entities;
using Website.Infrastructure.IRepositories;
using Website.Infrastructure.LogServices;
using Website.Models;

namespace Website.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;


        public CategoryController(IMediator mediator, ILoggerManager loggerManager, IMapper mapper, IDistributedCache distributedCache)
        {
            _mediator = mediator;
            _loggerManager = loggerManager;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }

        public IActionResult Index()
        {
            _loggerManager.LogDebug("ádfasdfhj");
            return new EmptyResult();
        }

        public async Task<IActionResult> SetCache(string strCacheKey)
        {
            await _distributedCache.SetStringAsync(strCacheKey, Guid.NewGuid().ToString());
            return new EmptyResult();
        }

        public async Task<string> GetCache(string strCacheKey)
        {
            string cacheData = await _distributedCache.GetStringAsync(strCacheKey);
            return cacheData;
        }

        public async Task<IActionResult> Get(int id)
        {
            var command = new GetCategoryByIdQuery { Id = id };
            var res = await _mediator.Send(command);
            return new EmptyResult();
        }

        public async Task<IActionResult> Create()
        {
            var str = Guid.NewGuid().ToString();
            var command = new CreateCategoryCommand { Name = "Cate " + str, Description = "Des " + str };
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
