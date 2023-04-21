namespace RestaurantManagement.Domain.DTO.Response
{
    public class CategoryResponseModel : BaseResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
