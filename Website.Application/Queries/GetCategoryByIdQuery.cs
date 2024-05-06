using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Domain.Entities;
using Website.Infrastructure.IRepositories;

namespace Website.Application.Queries
{
    public class GetCategoryByIdQuery : IRequest<Category>
    {
        public int Id { get; set; }
    }

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category>
    {
        #region contructor
        private readonly IRepository<Category> _categoryRepository;
        public GetCategoryByIdQueryHandler(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        #endregion

        public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetByIdAsync(request.Id);
        }
    }

    //public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category>
    //{
    //    #region contructor
    //    public GetCategoryByIdQueryHandler()
    //    {

    //    }
    //    #endregion

    //    public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    //    {
    //        var temp = request.Id;
    //        return new Category();
    //    }
    //}
}
