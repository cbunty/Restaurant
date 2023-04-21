using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Data.Interface;
using RestaurantManagement.Domain.DTO.Request;
using RestaurantManagement.Domain.DTO.Response;

namespace RestaurantManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantData _restaurantData;

        public RestaurantController(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }

        /// <summary>
        /// Create Restaurant
        /// </summary>
        /// <param name="restaurantRequestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<RestaurantResponseModel> Restaurant(RestaurantRequestModel restaurantRequestModel)
        {
            return await _restaurantData.AddRestaurant(restaurantRequestModel);
        }

        /// <summary>
        /// Update Restaurant
        /// </summary>
        /// <param name="id"></param>
        /// <param name="restaurantRequestModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<RestaurantResponseModel> Restaurant(int id, RestaurantRequestModel restaurantRequestModel)
        {
            restaurantRequestModel.Id = id;
            return await _restaurantData.UpdateRestaurant(restaurantRequestModel);
        }

        /// <summary>
        ///  Get Restaurant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<RestaurantResponseModel> Restaurant(int id)
        {
            return await _restaurantData.GetRestaurant(id);
        }

        /// <summary>
        /// Get Restaurants
        /// </summary>
        /// <param name="pageRequest"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResults<RestaurantResponseModel>> Restaurant([FromQuery] PageRequest pageRequest)
        {
            return await _restaurantData.GetRestaurants(pageRequest);
        }

        /// <summary>
        /// Delete Restaurant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _restaurantData.DeleteRestaurant(id);
        }
    }
}
