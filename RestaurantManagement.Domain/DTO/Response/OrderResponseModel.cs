namespace RestaurantManagement.Domain.DTO.Response
{
    public class OrderResponseModel :  AuditResponseModel
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDateTime { get; set; }
        public bool HasPaid { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatusResponseModel OrderStatus { get; set; }
        public List<OrderDetailResponseModel> OrderDetails { get; set; }
    }

    public class OrderDetailResponseModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MenuId { get; set; }

        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
