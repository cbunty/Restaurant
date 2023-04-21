namespace RestaurantManagement.Domain.DTO.Response
{
    public class RestaurantResponseModel : BaseResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string WebsiteUrl { get; set; }
        public List<MenuResponseModel> Menus { get; set; }
    }
}
