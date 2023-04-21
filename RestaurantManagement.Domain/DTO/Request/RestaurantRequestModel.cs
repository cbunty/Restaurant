using System.ComponentModel.DataAnnotations;

namespace RestaurantManagement.Domain.DTO.Request
{
    public class RestaurantRequestModel : SearchRequestModel
    {
        [StringLength(2000)]
        public string Description { get; set; }
        [StringLength(2000)]
        public string Address { get; set; }
        [StringLength(13)]
        public string PhoneNumber { get; set; }
        [StringLength(250)]
        public string WebsiteUrl { get; set; }
    }
}
