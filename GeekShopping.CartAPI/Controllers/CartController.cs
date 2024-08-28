using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Messages;
using GeekShopping.CartAPI.RabbitMQProducer;
using GeekShopping.CartAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CartController(ICartRepository repository, ICouponRepository couponRepository, IRabbitMQMessageProducer rabbitMQMessageProducer) : ControllerBase
{
    private readonly ICartRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    private readonly ICouponRepository _couponRepository = couponRepository ?? throw new ArgumentNullException(nameof(couponRepository));
    private readonly IRabbitMQMessageProducer _rabbitMQMessageProducer = rabbitMQMessageProducer ?? throw new ArgumentNullException(nameof(rabbitMQMessageProducer));

    [HttpGet("GetByUserId/{userId}")]
    public async Task<ActionResult<CartVO>> GetByUserId(string userId)
    {
        var cart = await _repository.GetByUserId(userId);
        if (cart == null) return NotFound();
        return Ok(cart);
    }

    [HttpPost("AddItem")]
    public async Task<ActionResult<CartVO>> AddItem(CartVO vo)
    {
        var cart = await _repository.SaveOrUpdate(vo);
        if (cart == null) return NotFound();
        return Ok(cart);
    }

    [HttpPut("Update")]
    public async Task<ActionResult<CartVO>> Update(CartVO vo)
    {
        var cart = await _repository.SaveOrUpdate(vo);
        if (cart == null) return NotFound();
        return Ok(cart);
    }

    [HttpDelete("RemoveItem/{id}")]
    public async Task<ActionResult<CartVO>> RemoveItem(int id)
    {
        var status = await _repository.RemoveItem(id);
        if (!status) return BadRequest();
        return Ok(status);
    }

    [HttpPost("ApplyCoupon")]
    public async Task<ActionResult<CartVO>> ApplyCoupon(CartVO vo)
    {
        var status = await _repository.ApplyCoupon(vo.CartHeader!.UserId!, vo.CartHeader.CouponCode!);
        if (!status) return NotFound();
        return Ok(status);
    }

    [HttpDelete("RemoveCoupon/{userId}")]
    public async Task<ActionResult<CartVO>> RemoveCoupon(string userId)
    {
        var status = await _repository.RemoveCoupon(userId);
        if (!status) return NotFound();
        return Ok(status);
    }

    [HttpPost("Checkout")]
    public async Task<ActionResult<CheckoutHeaderVO>> Checkout(CheckoutHeaderVO vo)
    {
        string token = Request.Headers.Authorization!;

        if (vo?.UserId == null) return BadRequest();
        var cart = await _repository.GetByUserId(vo.UserId);
        if (cart == null) return NotFound();
        if (!string.IsNullOrEmpty(vo.CouponCode))
        {
            CouponVO? coupon = await _couponRepository.GetCoupon(vo.CouponCode, token);
            if (vo.DiscountAmount != coupon!.DiscountAmount) return StatusCode(412);
        }
        vo.ListCartDetail = cart.ListCartDetail;
        vo.DateTime = DateTime.Now;

        _rabbitMQMessageProducer.SendMessage(vo, "checkoutqueue");

        return Ok(vo);
    }
}