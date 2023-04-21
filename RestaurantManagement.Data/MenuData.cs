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

namespace RestaurantManagement.Data
{
    public class MenuData : IMenuData
    {
        private readonly RestaurantDbContext _restaurantDbContext;
        private readonly IMapper _mapper;
        public MenuData(RestaurantDbContext restaurantDbContext, IMapper mapper)
        {
            _restaurantDbContext = restaurantDbContext;
            _mapper = mapper;
        }
        public async Task<MenuResponseModel> AddMenu(MenuRequestModel menuRequest)
        {
            if (await CheckIfAlreadyExists(menuRequest))
                throw new BadRequestException<Menu>($"Menu already exists with name {menuRequest.Name}");
            var menu = _mapper.Map<Menu>(menuRequest);
            _restaurantDbContext.Menus.Add(menu);
            return await SaveAndGetMenu(menu);
        }

        public async Task DeleteMenu(int id)
        {
            var menu = await GetMenuById(id);
            menu.StatusId = (byte)Domain.Enumerations.StatusEnum.InActive;
            _restaurantDbContext.Menus.Update(menu);
            await _restaurantDbContext.SaveChangesAsync();
        }

        public async Task<PagedResults<MenuResponseModel>> GetMenus(PageRequest pageRequest)
        {
            var query = _restaurantDbContext.Menus.WhereIf(!string.IsNullOrEmpty(pageRequest?.SearchParam), prd => prd.RestaurantId == Convert.ToInt32(pageRequest.SearchParam))
                                                   .Where(x => x.StatusId == (byte)Domain.Enumerations.StatusEnum.Active);

            var queryData = query.ProjectTo<MenuResponseModel>(_mapper.ConfigurationProvider);
            if (pageRequest.PageSize != 0)
                queryData = queryData.TakePage(pageRequest.PageNumber, pageRequest.PageSize);
            var totalRecords = query.Count();

            return new PagedResults<MenuResponseModel>
            {
                PageNumber = pageRequest.PageNumber,
                PageSize = pageRequest.PageSize == 0 ? totalRecords : pageRequest.PageSize,
                TotalNumberOfRecords = totalRecords,
                Results = await queryData.ToListAsync()
            };
        }

        public async Task<List<MenuResponseModel>> GetMenusByRestaurantId(int restaurantId)
        {
            var query = _restaurantDbContext.Menus.Where(x => x.StatusId == (byte)Domain.Enumerations.StatusEnum.Active && x.RestaurantId == restaurantId);

            return await query.ProjectTo<MenuResponseModel>(_mapper.ConfigurationProvider).ToListAsync();
            
        }

        public async Task<MenuResponseModel> GetMenu(int menuId)
        {
            var response = await _mapper.ProjectTo<MenuResponseModel>(_restaurantDbContext.Menus.Where(x => x.Id == menuId)).FirstOrDefaultAsync();
            if (response == null)
                throw new EntityNotFoundException<Menu>($"Menu not found for Id - {menuId}");
            return response;
        }

        public async Task<MenuResponseModel> UpdateMenu(MenuRequestModel menuRequest)
        {
            if (await CheckIfAlreadyExists(menuRequest))
                throw new BadRequestException<Menu>($"Menu already exists with name {menuRequest.Name}");
            var menu = await GetMenuById(menuRequest.Id);
            _mapper.Map(menuRequest, menu);
            _restaurantDbContext.Menus.Update(menu);
            return await SaveAndGetMenu(menu);
        }

        private async Task<bool> CheckIfAlreadyExists(MenuRequestModel menuRequestModel)
        {
            if (await _restaurantDbContext.Menus.AnyAsync(x => x.Name == menuRequestModel.Name && x.StatusId == (byte)Domain.Enumerations.StatusEnum.Active && x.Id != menuRequestModel.Id))
                return true;
            return false;
        }

        private async Task<Menu> GetMenuById(int id)
        {
            var menu = await _restaurantDbContext.Menus.FirstOrDefaultAsync(x => x.Id == id);
            if (menu == null)
                throw new EntityNotFoundException<Menu>($"Menu not found for Id - {id}");
            return menu;
        }

        private async Task<MenuResponseModel> SaveAndGetMenu(Menu menu)
        {
            await _restaurantDbContext.SaveChangesAsync();
            return await GetMenu(menu.Id);
        }
    }
}
