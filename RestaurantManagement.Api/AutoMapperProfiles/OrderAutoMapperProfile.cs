using AutoMapper;
using RestaurantManagement.Domain.DBModel;
using RestaurantManagement.Domain.DTO.Request;
using RestaurantManagement.Domain.DTO.Response;
using RestaurantManagement.Domain;

namespace RestaurantManagement.Api.AutoMapperProfiles
{
    public class OrderAutoMapperProfile : Profile
    {
        public OrderAutoMapperProfile()
        {
            CreateMap<OrderRequestModel, Order>()
              .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<OrderDetailRequestModel, OrderDetail>()
              .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<Order, OrderResponseModel>()
                .ForMember(x => x.OrderStatus, src => src.MapFrom(x => x.OrderStatus));

            CreateMap<OrderDetail, OrderDetailResponseModel>();

            CreateMap<Status, StatusResponseModel>()
                .ForMember(x => x.StatusId, src => src.MapFrom(x => x.Id));

            CreateMap<OrderStatus, OrderStatusResponseModel>()
                .ForMember(x => x.OrderStatusId, src => src.MapFrom(x => x.Id));
        }
    }
}
