using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CartController(ICartRepository repository) : ControllerBase
{
    private ICartRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    [HttpGet("GetByUserId/{userId}")]
    public async Task<ActionResult<CartVO>> GetByUserId(string userId)
    {
        var cart = await _repository.GetByUserId(userId);
        if (cart == null) return NotFound();
        return Ok(cart);
    }

    [HttpPost("Create")]
    public async Task<ActionResult<CartVO>> Crete(CartVO vo)
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

    [HttpDelete("Remove/{id}")]
    public async Task<ActionResult<CartVO>> Remove(int id)
    {
        var status = await _repository.Remove(id);
        if (!status) return BadRequest();
        return Ok(status);
    }
}