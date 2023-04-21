namespace RestaurantManagement.Domain.DTO.Response
{
    public class BaseResponseModel : AuditResponseModel
    {
        public StatusResponseModel Status { get; set; }
    }
}
