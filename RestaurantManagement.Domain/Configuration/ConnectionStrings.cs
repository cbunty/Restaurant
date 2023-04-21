namespace RestaurantManagement.Domain.Configuration
{
    public class ConnectionStrings
    {
        public ConnectionInfo RestaurantSqlDb { get; set; }
    }
    public class ConnectionInfo
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
    }
}
