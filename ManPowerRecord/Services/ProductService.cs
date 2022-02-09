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

        public string AddBrand(BrandModel brand)
        {
            SqlConnection connection = Database.Connect();
            connection.Open();
            using (SqlCommand command = new SqlCommand("INSERT INTO Brand(brand_id, brand_name) " +
                                                      "VALUES(@brand_id, @brand_name)", connection))
            {
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                command.Parameters.AddWithValue("@brand_id", brand.brand_id);
                command.Parameters.AddWithValue("@brand_name", brand.brand_name);
                command.ExecuteNonQuery();
            }
            connection.Close();
            return "Success";
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

        public string AddProduct(ProductModel product)
        {
            SqlConnection connection = Database.Connect();
            connection.Open();
            using (SqlCommand command = new SqlCommand("INSERT INTO Product(product_id, product_name, brand_id, product_price, product_unit) " +
                                                      "VALUES(@product_id, @product_name, @brand_id, @product_price, @product_unit)", connection))
            {
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                command.Parameters.AddWithValue("@product_id", product.product_id);
                command.Parameters.AddWithValue("@product_name", product.product_name);
                command.Parameters.AddWithValue("@brand_id", product.brand_id);
                command.Parameters.AddWithValue("@product_price", product.product_price);
                command.Parameters.AddWithValue("@product_unit", product.product_unit);
                command.ExecuteNonQuery();
            }
            connection.Close();
            return "Success";
        }
    }
}
