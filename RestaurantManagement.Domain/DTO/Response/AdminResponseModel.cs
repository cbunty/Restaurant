using System.ComponentModel.DataAnnotations;

namespace RestaurantManagement.Domain.DTO.Response
{
    public class AdminResponseModel : BaseResponseModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
