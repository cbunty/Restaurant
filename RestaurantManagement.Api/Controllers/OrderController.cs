using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Data.Interface;
using RestaurantManagement.Domain.DTO.Request;
using RestaurantManagement.Domain.DTO.Response;

namespace RestaurantManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderData _orderData;

        public OrderController(IOrderData orderData)
        {
            _orderData = orderData;
        }

        /// <summary>
        /// Create Order
        /// </summary>
        /// <param name="orderRequestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<OrderResponseModel> Order(OrderRequestModel orderRequestModel)
        {
            return await _orderData.AddOrder(orderRequestModel);
        }

        /// <summary>
        /// Update Order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderRequestModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<OrderResponseModel> Order(int id, OrderRequestModel orderRequestModel)
        {
            orderRequestModel.Id = id;
            return await _orderData.UpdateOrder(orderRequestModel);
        }

        /// <summary>
        ///  Get Order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<OrderResponseModel> Order(int id)
        {
            return await _orderData.GetOrder(id);
        }

        /// <summary>
        /// Get Orders
        /// </summary>
        /// <param name="pageRequest"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResults<OrderResponseModel>> Order([FromQuery] PageRequest pageRequest)
        {
            return await _orderData.GetOrders(pageRequest);
        }
    }
}
