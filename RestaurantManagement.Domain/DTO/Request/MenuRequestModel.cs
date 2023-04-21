using System.ComponentModel.DataAnnotations;

namespace RestaurantManagement.Domain.DTO.Request
{
    public class MenuRequestModel : SearchRequestModel
    {
        [StringLength(2000)]
        public string Description { get; set; }
        public int RestaurantId { get; set; }
        public int CategoryId { get; set; }
        [Required]
        [Range(0, 10000000, ErrorMessage = "The Price field must be greater than 0")]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
