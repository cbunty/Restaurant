using RestaurantManagement.Domain.DTO.Request;
using RestaurantManagement.Domain.DTO.Response;

namespace RestaurantManagement.Data.Interface
{
    public interface IOrderData
    {
        Task<PagedResults<OrderResponseModel>> GetOrders(PageRequest pageRequest);
        Task<OrderResponseModel> GetOrder(int orderId);
        Task<OrderResponseModel> AddOrder(OrderRequestModel orderRequest);
        Task<OrderResponseModel> UpdateOrder(OrderRequestModel orderRequest);
    }
}
