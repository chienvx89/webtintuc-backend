using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Application.Queries;
using Website.Domain.Entities;
using Website.Infrastructure.IRepositories;

namespace Website.Application.Commands.CategoryCommands
{
    public class CreateCategoryCommand: IRequest<Category>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
    {
        #region contructor
        private readonly IRepository<Category> _categoryRepository;
        public CreateCategoryCommandHandler(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        #endregion

        public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,
                //Id = 1,
                //Guid = Guid.NewGuid()
            };
           
            return await _categoryRepository.CreateAsync(category);
        }

    }


}
