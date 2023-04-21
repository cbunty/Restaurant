using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Data.Interface;
using RestaurantManagement.Domain.DTO.Request;
using RestaurantManagement.Domain.DTO.Response;

namespace RestaurantManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryData _categoryData;

        public CategoryController(ICategoryData categoryData)
        {
            _categoryData = categoryData;
        }

        /// <summary>
        /// Create Category
        /// </summary>
        /// <param name="categoryRequestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<CategoryResponseModel> Category(CategoryRequestModel categoryRequestModel)
        {
            return await _categoryData.AddCategory(categoryRequestModel);
        }

        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryRequestModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<CategoryResponseModel> Category(int id, CategoryRequestModel categoryRequestModel)
        {
            categoryRequestModel.Id = id;
            return await _categoryData.UpdateCategory(categoryRequestModel);
        }

        /// <summary>
        ///  Get Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<CategoryResponseModel> Category(int id)
        {
            return await _categoryData.GetCategory(id);
        }

        /// <summary>
        /// Get Categorys
        /// </summary>
        /// <param name="pageRequest"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResults<CategoryResponseModel>> Category([FromQuery] PageRequest pageRequest)
        {
            return await _categoryData.GetCategories(pageRequest);
        }

        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _categoryData.DeleteCategory(id);
        }
    }
}
