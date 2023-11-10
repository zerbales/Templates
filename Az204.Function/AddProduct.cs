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

namespace Az204.Function
{
    public static class AddProduct
    {
        [FunctionName("AddProduct")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Product data = JsonConvert.DeserializeObject<Product>(requestBody);
            SqlConnection conn = GetConnection();
            conn.Open();
            string query = $"INSERT INTO Products (ProductId, ProductName, Quantity) values (@p1, @p2,@p3)";
            using (SqlCommand cmd = new SqlCommand(query, conn)) {
                cmd.Parameters.Add("@p1", System.Data.SqlDbType.Int).Value = data.ProductID;
                cmd.Parameters.Add("@p2", System.Data.SqlDbType.VarChar).Value = data.ProductName;
                cmd.Parameters.Add("@p3", System.Data.SqlDbType.Int).Value = data.Quantity;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            return new OkObjectResult("Success added");
        }

        private static SqlConnection GetConnection()
        {
            string cs = "Server=tcp:zerbax-sqlserver.database.windows.net,1433;Initial Catalog=zerbax-db;Persist Security Info=False;User ID=zerbax;Password=PasswordAz204_;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            return new SqlConnection(cs);
        }
    }
}
