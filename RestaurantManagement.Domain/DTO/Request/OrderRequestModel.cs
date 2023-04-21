using RestaurantManagement.Domain.DBModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Domain.DTO.Request
{
    public class OrderRequestModel
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDateTime { get; set; }
        public bool HasPaid { get; set; } = false;
        public decimal TotalPrice { get; set; }
        public byte OrderStatusId { get; set; } = (byte)Enumerations.OrderStatusEnum.Pending;
        public List<OrderDetailRequestModel> OrderDetails { get; set; } = new List<OrderDetailRequestModel>();
    }

    public class OrderDetailRequestModel
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 10000000, ErrorMessage = "The Price field must be greater than 0")]
        public decimal UnitPrice { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The Quantity field must be greater or eqaul than 0")]
        public int Quantity { get; set; }
    }
}
