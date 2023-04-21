using AutoMapper;
using RestaurantManagement.Domain.DBModel;
using RestaurantManagement.Domain.DTO.Request;
using RestaurantManagement.Domain.DTO.Response;
using RestaurantManagement.Domain;

namespace RestaurantManagement.Api.AutoMapperProfiles
{
    public class MenuAutoMapperProfile : Profile
    {
        public MenuAutoMapperProfile()
        {
            CreateMap<MenuRequestModel, Menu>()
              .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<Menu, MenuResponseModel>()
                .ForMember(x => x.Status, src => src.MapFrom(x => x.Status));

            CreateMap<Status, StatusResponseModel>()
                .ForMember(x => x.StatusId, src => src.MapFrom(x => x.Id)); ;
        }
    }
}
