using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services;

public class ProductService(HttpClient httpClient) : IProductService
{
    private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    public const string BasePath = "api/v1/Product";

    public async Task<IEnumerable<ProductModel>> GetAll()
    {
        var response = await _httpClient.GetAsync(BasePath);
        return await response.ReadContentAs<List<ProductModel>>();
    }

    public async Task<ProductModel> GetById(long id)
    {
        var response = await _httpClient.GetAsync($"{BasePath}/{id}");
        return await response.ReadContentAs<ProductModel>();
    }

    public async Task<ProductModel> Create(ProductModel productModel)
    {
        var response = await _httpClient.PostAsJson($"{BasePath}", productModel);
        if (response.IsSuccessStatusCode)
            return await response.ReadContentAs<ProductModel>();
        else
            throw new Exception($"Something went wrong when calling API: {response.ReasonPhrase}");
    }

    public async Task<ProductModel> Update(ProductModel productModel)
    {
        var response = await _httpClient.PutAsJson($"{BasePath}", productModel);
        if (response.IsSuccessStatusCode)
            return await response.ReadContentAs<ProductModel>();
        else
            throw new Exception($"Something went wrong when calling API: {response.ReasonPhrase}");
    }

    public async Task<bool> Delete(long id)
    {
        var response = await _httpClient.DeleteAsync($"{BasePath}/{id}");
        if (response.IsSuccessStatusCode)
            return await response.ReadContentAs<bool>();
        else
            throw new Exception($"Something went wrong when calling API: {response.ReasonPhrase}");
    }
}