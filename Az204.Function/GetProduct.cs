using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Az204.Function
{
    public static class GetProduct
    {
        [FunctionName("GetProductById")]
        public static async Task<IActionResult> RunGetProductById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        { 
            int id = int.Parse(req.Query["id"]);
            SqlConnection conn = GetConnection();
           
            string query = $"SELECT * FROM Products WHERE ProductId = {id}";
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);

            Product product = new Product();
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    product.ProductID = reader.GetInt32(0);
                    product.ProductName = reader.GetString(1);
                    product.Quantity = reader.GetInt32(2);
                    return new OkObjectResult(product);
                }
            }
            catch (Exception ex) {
                var response = "Not found";
                return new OkObjectResult(response);

            }
            finally {
                conn.Close();
            }
        }

        [FunctionName("GetProducts")]
        public static async Task<IActionResult> RunGetProducts(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
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
            return new OkObjectResult(JsonConvert.SerializeObject(products));
        }

        private static SqlConnection GetConnection()
        {
            string cs = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_SQLConnection");
            return new SqlConnection(cs);
        }
    }
}
