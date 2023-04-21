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
    public class RestaurantData : IRestaurantData
    {
        private readonly RestaurantDbContext _restaurantDbContext;
        private readonly IMapper _mapper;
        public RestaurantData(RestaurantDbContext restaurantDbContext, IMapper mapper)
        {
            _restaurantDbContext = restaurantDbContext;
            _mapper = mapper;
        }
        public async Task<RestaurantResponseModel> AddRestaurant(RestaurantRequestModel restaurantRequest)
        {
            if (await CheckIfAlreadyExists(restaurantRequest))
                throw new BadRequestException<Restaurant>($"Restaurant already exists with name {restaurantRequest.Name}");
            var restaurant = _mapper.Map<Restaurant>(restaurantRequest);
            _restaurantDbContext.Restaurants.Add(restaurant);
            return await SaveAndGetRestaurant(restaurant);
        }

        public async Task DeleteRestaurant(int id)
        {
            var restaurant = await GetRestaurantById(id);
            restaurant.StatusId = (byte)Domain.Enumerations.StatusEnum.InActive;
            _restaurantDbContext.Restaurants.Update(restaurant);
            await _restaurantDbContext.SaveChangesAsync();
        }

        public async Task<PagedResults<RestaurantResponseModel>> GetRestaurants(PageRequest pageRequest)
        {
            var query = _restaurantDbContext.Restaurants.WhereIf(!string.IsNullOrEmpty(pageRequest?.SearchParam), prd => prd.Name.Contains(pageRequest.SearchParam) || prd.Description.Contains(pageRequest.SearchParam))
                                                   .Where(x => x.StatusId == (byte)Domain.Enumerations.StatusEnum.Active);

            var queryData = query.ProjectTo<RestaurantResponseModel>(_mapper.ConfigurationProvider);
            if (pageRequest.PageSize != 0)
                queryData = queryData.TakePage(pageRequest.PageNumber, pageRequest.PageSize);
            var totalRecords = query.Count();

            return new PagedResults<RestaurantResponseModel>
            {
                PageNumber = pageRequest.PageNumber,
                PageSize = pageRequest.PageSize == 0 ? totalRecords : pageRequest.PageSize,
                TotalNumberOfRecords = totalRecords,
                Results = await queryData.ToListAsync()
            };
        }

        public async Task<RestaurantResponseModel> GetRestaurant(int restaurantId)
        {
            var response = await _mapper.ProjectTo<RestaurantResponseModel>(_restaurantDbContext.Restaurants.Where(x => x.Id == restaurantId)).FirstOrDefaultAsync();
            if (response == null)
                throw new EntityNotFoundException<Restaurant>($"Restaurant not found for Id - {restaurantId}");
            return response;
        }

        public async Task<RestaurantResponseModel> UpdateRestaurant(RestaurantRequestModel restaurantRequest)
        {
            if (await CheckIfAlreadyExists(restaurantRequest))
                throw new BadRequestException<Restaurant>($"Restaurant already exists with name {restaurantRequest.Name}");
            var restaurant = await GetRestaurantById(restaurantRequest.Id);
            _mapper.Map(restaurantRequest, restaurant);
            _restaurantDbContext.Restaurants.Update(restaurant);
            return await SaveAndGetRestaurant(restaurant);
        }

        private async Task<bool> CheckIfAlreadyExists(RestaurantRequestModel restaurantRequestModel)
        {
            if (await _restaurantDbContext.Menus.AnyAsync(x => x.Name == restaurantRequestModel.Name && x.StatusId == (byte)Domain.Enumerations.StatusEnum.Active && x.Id != restaurantRequestModel.Id))
                return true;
            return false;
        }

        private async Task<Restaurant> GetRestaurantById(int id)
        {
            var restaurant = await _restaurantDbContext.Restaurants.FirstOrDefaultAsync(x => x.Id == id);
            if (restaurant == null)
                throw new EntityNotFoundException<Restaurant>($"Restaurant not found for Id - {id}");
            return restaurant;
        }

        private async Task<RestaurantResponseModel> SaveAndGetRestaurant(Restaurant restaurant)
        {
            await _restaurantDbContext.SaveChangesAsync();
            return await GetRestaurant(restaurant.Id);
        }
    }
}
