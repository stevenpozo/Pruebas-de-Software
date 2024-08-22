using System.Data.SqlClient;
using System.Data;
using Laboratorio1_3P.Models;

namespace Laboratorio1_3P.Data
{
    public class ClienteSqlPruebaAccessLayer
    {
        string connectionString = "Data Source=STEVEN; Initial Catalog= Products; user ID=sa; Password=sa";

        // Método para extraer la lista de todos los productos
        public IEnumerable<ClienteSqlPrueba> GetAllProducts()
        {
            List<ClienteSqlPrueba> lst = new List<ClienteSqlPrueba>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("product_SelectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ClienteSqlPrueba product = new ClienteSqlPrueba();
                    product.ProductId = Convert.ToInt32(reader["ProductId"]);
                    product.ProductName = reader["ProductName"].ToString();
                    product.Category = reader["Category"].ToString();
                    product.Price = Convert.ToDecimal(reader["Price"]);
                    product.StockQuantity = Convert.ToInt32(reader["StockQuantity"]);

                    lst.Add(product);
                }
                con.Close();
            }
            return lst;
        }

        // Método para imprimir los datos en la consola
        public void PrintAllProducts()
        {
            IEnumerable<ClienteSqlPrueba> products = GetAllProducts();
            foreach (ClienteSqlPrueba product in products)
            {
                Console.WriteLine("Datos SQL Server");
                Console.WriteLine("ProductId: " + product.ProductId);
                Console.WriteLine("ProductName: " + product.ProductName);
                Console.WriteLine("Category: " + product.Category);
                Console.WriteLine("Price: " + product.Price);
                Console.WriteLine("StockQuantity: " + product.StockQuantity);
                Console.WriteLine("----------------------------");
            }
        }

        // Método para verificar si un producto ya existe
        public bool ProductExists(string productName)
        {
            bool exists = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(1) FROM Products WHERE ProductName = @ProductName";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductName", productName);

                conn.Open();
                exists = (int)cmd.ExecuteScalar() > 0;
                conn.Close();
            }

            return exists;
        }
        // Método para añadir un nuevo producto
        public void AddProduct(ClienteSqlPrueba product)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Aquí se está creando un comando SQL para ejecutar el procedimiento almacenado "product_Insert"
                SqlCommand cmd = new SqlCommand("product_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Category", product.Category);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // Método para obtener un producto por su ID
        public ClienteSqlPrueba GetProductById(int id)
        {
            ClienteSqlPrueba product = new ClienteSqlPrueba();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("product_SelectByID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductId", id);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    product.ProductId = Convert.ToInt32(reader["ProductId"]);
                    product.ProductName = reader["ProductName"].ToString();
                    product.Category = reader["Category"].ToString();
                    product.Price = Convert.ToDecimal(reader["Price"]);
                    product.StockQuantity = Convert.ToInt32(reader["StockQuantity"]);
                }
                con.Close();
            }
            return product;
        }

        // Método para obtener un producto por su nombre 
        public ClienteSqlPrueba GetProductByName(string productName)
        {
            ClienteSqlPrueba product = null; 
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetProductByName", con); 
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductName", productName);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    product = new ClienteSqlPrueba
                    {
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        ProductName = reader["ProductName"].ToString(),
                        Category = reader["Category"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        StockQuantity = Convert.ToInt32(reader["StockQuantity"])
                    };
                }
                con.Close();
            }
            return product;
        }


        // Método para actualizar un producto
        public void UpdateProduct(ClienteSqlPrueba product)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("product_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Category", product.Category);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // Método para eliminar un producto por su ID
        public void DeleteProduct(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("product_Delete", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductId", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // Método para eliminar un producto por su nombre
        public void DeleteProductByName(string productName)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("product_DeleteByName", con); // Cambia a un procedimiento almacenado que acepte nombre del producto
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductName", productName);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }



    }
}
