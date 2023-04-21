using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Data.Contexts;
using RestaurantManagement.Data.Interface;
using RestaurantManagement.Domain.DBModel;
using RestaurantManagement.Domain.DTO.Response;
using RestaurantManagement.Domain.Exceptions;

namespace RestaurantManagement.Data
{
    public class AdminData : IAdminData
    {
        private readonly RestaurantDbContext _restaurantDbContext;
        private readonly IMapper _mapper;
        public AdminData(RestaurantDbContext restaurantDbContext, IMapper mapper)
        {
            _restaurantDbContext = restaurantDbContext;
            _mapper = mapper;
        }
        public async Task<AdminResponseModel> GetAdmin(string userName, string password)
        {
            var response = await _mapper.ProjectTo<AdminResponseModel>(_restaurantDbContext.Users.Where(x => x.UserName.ToLower() == userName.ToLower())).FirstOrDefaultAsync();
            if (response == null)
                throw new EntityNotFoundException<Restaurant>($"User not found for username - {userName}");

            var isPasswordCorrect = response.Password == password;

            if(!isPasswordCorrect)
                throw new EntityNotFoundException<Restaurant>($"password not correct for username - {userName}");

            return response;

        }
    }
}
