namespace SportsStore.Models
{
    public interface IOrderRepository
    {
        IQueryable<Order> Products { get; }
    }
}