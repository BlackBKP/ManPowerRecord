using ManPowerRecord.Interfaces;
using ManPowerRecord.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Services
{
    public class ProductService : IProduct
    {
        IConnectDB Database;

        public ProductService()
        {
            this.Database = new ConnectDB();
        }

        public List<BrandModel> GetBrands()
        {
            List<BrandModel> brands = new List<BrandModel>();
            SqlConnection connection = Database.Connect();
            connection.Open();

            string string_command = "SELECT * FROM Brand";
            SqlCommand command = new SqlCommand(string_command, connection);
            SqlDataReader data_reader = command.ExecuteReader();

            if (data_reader.HasRows)
            {
                while (data_reader.Read())
                {
                    BrandModel brand = new BrandModel()
                    {
                        brand_id = data_reader["brand_id"] != DBNull.Value ? data_reader["brand_id"].ToString() : "",
                        brand_name = data_reader["brand_name"] != DBNull.Value ? data_reader["brand_name"].ToString() : "",
                    };
                    brands.Add(brand);
                }
                data_reader.Close();
            }

            connection.Close();
            return brands;
        }

        public List<ProductModel> GetProducts()
        {
            List<ProductModel> products = new List<ProductModel>();
            SqlConnection connection = Database.Connect();
            connection.Open();

            string string_command = "SELECT * FROM Product";
            SqlCommand command = new SqlCommand(string_command, connection);
            SqlDataReader data_reader = command.ExecuteReader();

            if (data_reader.HasRows)
            {
                while (data_reader.Read())
                {
                    ProductModel product = new ProductModel()
                    {
                        product_id = data_reader["product_id"] != DBNull.Value ? data_reader["product_id"].ToString() : "",
                        product_name = data_reader["product_name"] != DBNull.Value ? data_reader["product_name"].ToString() : "",
                        brand_id = data_reader["brand_id"] != DBNull.Value ? data_reader["brand_id"].ToString() : "",
                        product_price = data_reader["brand_id"] != DBNull.Value ? Convert.ToInt32(data_reader["brand_id"]) : 0,
                        product_unit = data_reader["product_unit"] != DBNull.Value ? data_reader["product_unit"].ToString() : "",
                    };
                    products.Add(product);
                }
                data_reader.Close();
            }

            connection.Close();
            return products;
        }
    }
}
