using Microsoft.EntityFrameworkCore;

namespace GeekShopping.OrderAPI.Model.Context;

public class MySQLContext : DbContext
{
    public MySQLContext() { }

    public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

    public DbSet<OrderDetail> OrderDetail { get; set; }
    public DbSet<OrderHeader> OrderHeader { get; set; }
}