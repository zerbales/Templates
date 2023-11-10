using Az204.WebApp.Models;
using System.Data.SqlClient;
namespace Az204.WebApp.Services
{
    public class ProductService
    {
        private static string db_source = "zerbax.database.windows.net";
        private static string db_user = "zerbax";
        private static string db_password = "PasswordAz204_";
        private static string db_db = "zerbax-db";
        private readonly IConfiguration configuration;

        public ProductService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        private SqlConnection GetConnection()
        {
            
            return new SqlConnection(this.configuration.GetConnectionString("SQLConnection"));
        }
        public List<Product> GetProducts() { 
            SqlConnection conn = GetConnection();
            List<Product> products = new List<Product>();
            string query = "SELECT * FROM Products";
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2),
                    };
                    products.Add(product);
                }
            }
            conn.Close();
            return products;
            
        }
    }
}
