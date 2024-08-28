using GeekShopping.CartAPI.Data.ValueObjects;

namespace GeekShopping.CartAPI.Repository.Interfaces;

public interface ICouponRepository
{
    Task<CouponVO?> GetCoupon(string couponCode, string token);
}