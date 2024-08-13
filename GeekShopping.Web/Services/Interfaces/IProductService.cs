using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductModel>> GetAll();
    Task<ProductModel> GetById(long id);
    Task<ProductModel> Create(ProductModel productModel);
    Task<ProductModel> Update(ProductModel productModel);
    Task<bool> Delete(long id);
}