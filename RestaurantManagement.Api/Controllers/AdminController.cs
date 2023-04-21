using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Data.Interface;
using RestaurantManagement.Domain.DTO.Response;

namespace RestaurantManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminData _adminData;

        public AdminController(IAdminData adminData)
        {
            _adminData = adminData;
        }

        /// <summary>
        /// Create Menu
        /// </summary>
        /// <param name="menuRequestModel"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AdminResponseModel> Admin(string userName, string password)
        {
            return await _adminData.GetAdmin(userName, password);
        }
    }
}
