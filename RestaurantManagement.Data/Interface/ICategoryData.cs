using RestaurantManagement.Domain.DTO.Request;
using RestaurantManagement.Domain.DTO.Response;

namespace RestaurantManagement.Data.Interface
{
    public interface ICategoryData
    {
        Task<PagedResults<CategoryResponseModel>> GetCategories(PageRequest pageRequest);
        Task<CategoryResponseModel> GetCategory(int productId);
        Task<CategoryResponseModel> AddCategory(CategoryRequestModel categoryRequest);
        Task<CategoryResponseModel> UpdateCategory(CategoryRequestModel categoryRequest);
        Task DeleteCategory(int id);
    }
}
