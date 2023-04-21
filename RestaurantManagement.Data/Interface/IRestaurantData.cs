using RestaurantManagement.Domain.DTO.Request;
using RestaurantManagement.Domain.DTO.Response;

namespace RestaurantManagement.Data.Interface
{
    public interface IRestaurantData
    {
        Task<PagedResults<RestaurantResponseModel>> GetRestaurants(PageRequest pageRequest);
        Task<RestaurantResponseModel> GetRestaurant(int restaurantId);
        Task<RestaurantResponseModel> AddRestaurant(RestaurantRequestModel restaurantRequest);
        Task<RestaurantResponseModel> UpdateRestaurant(RestaurantRequestModel restaurantRequest);
        Task DeleteRestaurant(int id);
    }
}
