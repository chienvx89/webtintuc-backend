using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Application.Dtos;
using Website.Application.Queries;
using Website.Domain.Entities;
using Website.Infrastructure.IRepositories;

namespace Website.Application.Commands.CategoryCommands
{
    public class CreateCategoryCommand: IRequest<CategoryDTO>
    {

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDTO>
    {
        #region contructor
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        public CreateCategoryCommandHandler(IRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        #endregion

        public async Task<CategoryDTO> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,
            };
            var categoryAdd = await _categoryRepository.CreateAsync(category);
            var res = _mapper.Map<CategoryDTO>(categoryAdd);

            return res;
        }

    }


}
