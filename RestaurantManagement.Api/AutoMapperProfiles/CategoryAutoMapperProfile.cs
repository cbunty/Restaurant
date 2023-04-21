using AutoMapper;
using RestaurantManagement.Domain.DTO.Response;
using RestaurantManagement.Domain;
using RestaurantManagement.Domain.DTO.Request;
using RestaurantManagement.Domain.DBModel;

namespace RestaurantManagement.Api.AutoMapperProfiles
{
    public class CategoryAutoMapperProfile : Profile
    {
        public CategoryAutoMapperProfile()
        {
            CreateMap<CategoryRequestModel, Category>()
              .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<Category, CategoryResponseModel>()
                .ForMember(x => x.Status, src => src.MapFrom(x => x.Status));

            CreateMap<Status, StatusResponseModel>()
                .ForMember(x => x.StatusId, src => src.MapFrom(x => x.Id));

            CreateMap<User, AdminResponseModel>()
                .ForMember(x => x.Status, src => src.MapFrom(x => x.Status));
        }
    }
}
