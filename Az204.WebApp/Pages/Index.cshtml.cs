using Az204.WebApp.Models;
using Az204.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Az204.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<Product> Products { get; set; }

        public void OnGet()
        {
            ProductService ps = new ProductService();
            Products = ps.GetProducts();

        }
    }
}