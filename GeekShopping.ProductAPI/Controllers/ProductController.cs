using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ProductController(IProductRepository repository) : ControllerBase
{
    private readonly IProductRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductVO>>> GetAll()
    {
        var products = await _repository.GetAll();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductVO>> GetById(long id)
    {
        var product = await _repository.GetById(id);
        if (product.Id <= 0)
            return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductVO>> Create(ProductVO productVO)
    {
        if (productVO == null)
            return BadRequest();
        var product = await _repository.Create(productVO);
        return Ok(product);
    }

    [HttpPut]
    public async Task<ActionResult<ProductVO>> Update(ProductVO productVO)
    {
        if (productVO == null)
            return BadRequest();
        var product = await _repository.Update(productVO);
        return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(long id)
    {
       var status = await _repository.Delete(id);
        if(!status) return BadRequest();
        return Ok(status);
    }
}