using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Data.Interface;
using RestaurantManagement.Domain.DTO.Request;
using RestaurantManagement.Domain.DTO.Response;

namespace RestaurantManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuData _menuData;

        public MenuController(IMenuData menuData)
        {
            _menuData = menuData;
        }

        /// <summary>
        /// Create Menu
        /// </summary>
        /// <param name="menuRequestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MenuResponseModel> Menu(MenuRequestModel menuRequestModel)
        {
            return await _menuData.AddMenu(menuRequestModel);
        }

        /// <summary>
        /// Update Menu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="menuRequestModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<MenuResponseModel> Menu(int id, MenuRequestModel menuRequestModel)
        {
            menuRequestModel.Id = id;
            return await _menuData.UpdateMenu(menuRequestModel);
        }

        /// <summary>
        ///  Get Menu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<MenuResponseModel> Menu(int id)
        {
            return await _menuData.GetMenu(id);
        }

        /// <summary>
        /// Get Menus
        /// </summary>
        /// <param name="pageRequest"></param>
        /// <returns></returns>
        [HttpGet("restaurant/{id}")]
        public async Task<List<MenuResponseModel>> GetMenusByRestaurantId(int id)
        {
            return await _menuData.GetMenusByRestaurantId(id);
        }

        /// <summary>
        /// Delete Menu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _menuData.DeleteMenu(id);
        }
    }
}
