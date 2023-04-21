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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Data
{
    public class OrderData : IOrderData
    {
        private readonly RestaurantDbContext _restaurantDbContext;
        private readonly IMapper _mapper;
        public OrderData(RestaurantDbContext restaurantDbContext, IMapper mapper)
        {
            _restaurantDbContext = restaurantDbContext;
            _mapper = mapper;
        }
        public async Task<OrderResponseModel> AddOrder(OrderRequestModel orderRequest)
        {
            ValidateOrder(orderRequest);
            var order = _mapper.Map<Order>(orderRequest);

            _restaurantDbContext.Orders.Add(order);
            return await SaveAndGetOrder(order);

        }

        private void ValidateOrder(OrderRequestModel orderRequest)
        {
            orderRequest.OrderDetails.ToList().ForEach(x =>
            {
                var menu = _restaurantDbContext.Menus.Where(x => x.Id == x.Id).FirstOrDefault();
                if (menu == null)
                    throw new EntityNotFoundException<Order>($"Menu not exists.");

                if (menu.Quantity < x.Quantity)
                    throw new BadRequestException<Order>($"No more stock for menu.");
            });
        }

        public async Task<PagedResults<OrderResponseModel>> GetOrders(PageRequest pageRequest)
        {
            var query = _restaurantDbContext.Orders.WhereIf(!string.IsNullOrEmpty(pageRequest?.SearchParam), prd => prd.ModifiedBy.Contains(pageRequest.SearchParam));

            var queryData = query.ProjectTo<OrderResponseModel>(_mapper.ConfigurationProvider);
            if (pageRequest.PageSize != 0)
                queryData = queryData.TakePage(pageRequest.PageNumber, pageRequest.PageSize);
            var totalRecords = query.Count();

            return new PagedResults<OrderResponseModel>
            {
                PageNumber = pageRequest.PageNumber,
                PageSize = pageRequest.PageSize == 0 ? totalRecords : pageRequest.PageSize,
                TotalNumberOfRecords = totalRecords,
                Results = await queryData.ToListAsync()
            };
        }

        public async Task<OrderResponseModel> GetOrder(int orderId)
        {
            var response = await _mapper.ProjectTo<OrderResponseModel>(_restaurantDbContext.Orders.Where(x => x.Id == orderId)).FirstOrDefaultAsync();
            if (response == null)
                throw new EntityNotFoundException<Order>($"Order not found for Id - {orderId}");
            return response;
        }

        public async Task<OrderResponseModel> UpdateOrder(OrderRequestModel orderRequest)
        {
            var menuIds = orderRequest.OrderDetails.Select(x => x.MenuId);
            var menus = await _restaurantDbContext.Menus.Where(x => menuIds.Contains(x.Id)).ToListAsync();

            if (menus.Count != orderRequest.OrderDetails.Count)
                throw new EntityNotFoundException<Order>($"Menu not exists.");


            var order = await GetOrderById(orderRequest.Id);
            orderRequest.OrderDetails.ToList().ForEach(x =>
            {
                var menu = _restaurantDbContext.Menus.Where(x => x.Id == x.Id).FirstOrDefault();
                if (menu == null)
                    throw new EntityNotFoundException<Order>($"Menu not exists.");

                var orderDetail = order.OrderDetails.FirstOrDefault(x => x.MenuId == menu.Id);

                if (orderDetail != null && orderDetail.Quantity != x.Quantity)
                    throw new BadRequestException<Order>($"No more stock for menu.");
            });
            _mapper.Map(orderRequest, order);
            _restaurantDbContext.Orders.Update(order);
            return await SaveAndGetOrder(order);
        }

        private async Task<Order> GetOrderById(int id)
        {
            var order = await _restaurantDbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (order == null)
                throw new EntityNotFoundException<Order>($"Order not found for Id - {id}");
            return order;
        }

        private async Task<OrderResponseModel> SaveAndGetOrder(Order order)
        {
            await _restaurantDbContext.SaveChangesAsync();
            return await GetOrder(order.Id);
        }
    }
}
