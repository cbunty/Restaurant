using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Data.Contexts;
using RestaurantManagement.Data.Extension;
using RestaurantManagement.Data.Interface;
using RestaurantManagement.Domain.DBModel;
using RestaurantManagement.Domain.DTO.Request;
using RestaurantManagement.Domain.DTO.Response;
using RestaurantManagement.Domain.Exceptions;

namespace RestaurantManagement.Data
{
    public class CategoryData : ICategoryData
    {
        private readonly RestaurantDbContext _restaurantDbContext;
        private readonly IMapper _mapper;
        public CategoryData(RestaurantDbContext restaurantDbContext, IMapper mapper)
        {
            _restaurantDbContext = restaurantDbContext;
            _mapper = mapper;
        }
        public async Task<CategoryResponseModel> AddCategory(CategoryRequestModel categoryRequest)
        {
            if (await CheckIfAlreadyExists(categoryRequest))
                throw new BadRequestException<Category>($"Category already exists with name {categoryRequest.Name}");
            var category = _mapper.Map<Category>(categoryRequest);
            _restaurantDbContext.Categories.Add(category);
            return await SaveAndGetCategory(category);
        }

        public async Task DeleteCategory(int id)
        {
            var category = await GetCategoryById(id);
            category.StatusId = (byte)Domain.Enumerations.StatusEnum.InActive;
            _restaurantDbContext.Categories.Update(category);
            await _restaurantDbContext.SaveChangesAsync();
        }

        public async Task<PagedResults<CategoryResponseModel>> GetCategories(PageRequest pageRequest)
        {
            var query = _restaurantDbContext.Categories.WhereIf(!string.IsNullOrEmpty(pageRequest?.SearchParam), prd => prd.Name.Contains(pageRequest.SearchParam) || prd.Description.Contains(pageRequest.SearchParam))
                                                   .Where(x => x.StatusId == (byte)Domain.Enumerations.StatusEnum.Active);

            var queryData = query.ProjectTo<CategoryResponseModel>(_mapper.ConfigurationProvider);
            if (pageRequest.PageSize != 0)
                queryData = queryData.TakePage(pageRequest.PageNumber, pageRequest.PageSize);
            var totalRecords = query.Count();

            return new PagedResults<CategoryResponseModel>
            {
                PageNumber = pageRequest.PageNumber,
                PageSize = pageRequest.PageSize == 0 ? totalRecords : pageRequest.PageSize,
                TotalNumberOfRecords = totalRecords,
                Results = await queryData.ToListAsync()
            };
        }

        public async Task<CategoryResponseModel> GetCategory(int categoryId)
        {
            var response = await _mapper.ProjectTo<CategoryResponseModel>(_restaurantDbContext.Categories.Where(x => x.Id == categoryId)).FirstOrDefaultAsync();
            if (response == null)
                throw new EntityNotFoundException<Category>($"Category not found for Id - {categoryId}");
            return response;
        }

        public async Task<CategoryResponseModel> UpdateCategory(CategoryRequestModel categoryRequest)
        {
            if (await CheckIfAlreadyExists(categoryRequest))
                throw new BadRequestException<Category>($"Category already exists with name {categoryRequest.Name}");
            var category = await GetCategoryById(categoryRequest.Id);
            _mapper.Map(categoryRequest, category);
            _restaurantDbContext.Categories.Update(category);
            return await SaveAndGetCategory(category);
        }

        private async Task<bool> CheckIfAlreadyExists(CategoryRequestModel categoryRequestModel)
        {
            if (await _restaurantDbContext.Categories.AnyAsync(x => x.Name == categoryRequestModel.Name && x.StatusId == (byte)Domain.Enumerations.StatusEnum.Active && x.Id != categoryRequestModel.Id))
                return true;
            return false;
        }

        private async Task<Category> GetCategoryById(int id)
        {
            var category = await _restaurantDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                throw new EntityNotFoundException<Category>($"Category not found for Id - {id}");
            return category;
        }

        private async Task<CategoryResponseModel> SaveAndGetCategory(Category category)
        {
            await _restaurantDbContext.SaveChangesAsync();
            return await GetCategory(category.Id);
        }
    }
}
