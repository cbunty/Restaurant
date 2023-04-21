using RestaurantManagement.Domain.DTO.Request;
using RestaurantManagement.Domain.DTO.Response;

namespace RestaurantManagement.Data.Interface
{
    public interface IMenuData
    {
        Task<PagedResults<MenuResponseModel>> GetMenus(PageRequest pageRequest);
        Task<MenuResponseModel> GetMenu(int menuId);
        Task<MenuResponseModel> AddMenu(MenuRequestModel menuRequest);
        Task<MenuResponseModel> UpdateMenu(MenuRequestModel menuRequest);
        Task DeleteMenu(int id);
        Task<List<MenuResponseModel>> GetMenusByRestaurantId(int restaurantId);
    }
}
