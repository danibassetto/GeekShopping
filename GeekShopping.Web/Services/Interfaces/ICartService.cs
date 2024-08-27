using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.Interfaces;

public interface ICartService
{
    Task<CartViewModel?> GetByUserId(string userId, string token);
    Task<CartViewModel?> AddItem(CartViewModel cart, string token);
    Task<CartViewModel?> Update(CartViewModel cart, string token);
    Task<bool> Remove(long cartId, string token);

    Task<bool> ApplyCoupon(CartViewModel cart, string couponCode, string token);
    Task<bool> RemoveCoupon(string userId, string token);
    Task<bool> Clear(string userId, string token);

    Task<CartViewModel> Checkout(CartHeaderViewModel cartHeader, string token);
}