using GeekShopping.OrderAPI.Model;
using GeekShopping.OrderAPI.Model.Context;
using GeekShopping.OrderAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Repository;

public class OrderRepository(DbContextOptions<MySQLContext> context) : IOrderRepository
{
    private readonly DbContextOptions<MySQLContext> _context = context;

    public async Task<bool> Create(OrderHeader header)
    {
        if(header == null) return false;
        await using var _db = new MySQLContext(_context);
        _db.OrderHeader.Add(header);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task UpdatePaymentStatus(long orderHeaderId, bool status)
    {
        await using var _db = new MySQLContext(_context);
        var header = await _db.OrderHeader.FirstOrDefaultAsync(o => o.Id == orderHeaderId);
        if (header != null)
        {
            header.PaymentStatus = status;
            await _db.SaveChangesAsync();
        };
    }
}