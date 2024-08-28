using GeekShopping.OrderAPI.Model;

namespace GeekShopping.OrderAPI.Repository.Interfaces;

public interface IOrderRepository
{
    Task<bool> Create(OrderHeader header);
    Task UpdatePaymentStatus(long orderHeaderId, bool paid);
}