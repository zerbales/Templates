using Az204.WebApp.Models;

namespace Az204.WebApp.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}