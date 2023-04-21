using RestaurantManagement.Domain.Enumerations;

namespace RestaurantManagement.Domain.DTO.Request
{
    public class BaseRequestModel : AuditRequestModel
    {
        public byte? StatusId { get; set; } = (byte)StatusEnum.Active;
    }
}
