namespace RestaurantManagement.Domain.Exceptions
{
    public class BadRequestException<T> : ApplicationException
    {
        public BadRequestException(string message) : base(message)
        {
        }
    }
}
