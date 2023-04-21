using RestaurantManagement.Domain.DTO.Response;

namespace RestaurantManagement.Data.Interface
{
    public interface IAdminData
    {
        Task<AdminResponseModel> GetAdmin(string userName, string password);
    }
}
