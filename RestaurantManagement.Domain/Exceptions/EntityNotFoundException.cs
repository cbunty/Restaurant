namespace RestaurantManagement.Domain.Exceptions
{
    public class EntityNotFoundException<T> : ApplicationException
    {
        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
}
