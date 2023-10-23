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

        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_password;

            _builder.InitialCatalog = db_db;
            return new SqlConnection(_builder.ConnectionString);
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
