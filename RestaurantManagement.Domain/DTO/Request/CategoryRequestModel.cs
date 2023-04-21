using System.ComponentModel.DataAnnotations;

namespace RestaurantManagement.Domain.DTO.Request
{
    public class CategoryRequestModel : SearchRequestModel
    {
        [StringLength(2000)]
        public string Description { get; set; }
    }
    public class SearchRequestModel : BaseRequestModel
    {
        public int Id { get; set; }
        [StringLength(500)]
        [Required]
        public string Name { get; set; }
    }
}
