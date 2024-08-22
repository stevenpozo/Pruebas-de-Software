using Laboratorio1_3P.Data;
using Laboratorio1_3P.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Laboratorio1_3P.Data
{
    public class ClientSqlDataAccessLayer
    {
        // Realizar la conexión a la base de datos, conection string
        string connectionString = "Data Source=STEVEN; Initial Catalog= dbproductos; user ID=sa; Password=sa";

        // Metodo para extraer la lista
        public IEnumerable<ClientSql> GetAllClientes()
        {
            List<ClientSql> lst = new List<ClientSql>();
            // Conexion de la capa de datos al modelo
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Se debe tener una transaccion en sqlserver
                SqlCommand cmd = new SqlCommand("cliente_SelectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ClientSql client = new ClientSql();
                    client.Codigo = Convert.ToInt32(reader["Codigo"]);
                    client.Cedula = reader["Cedula"].ToString();
                    client.Apellidos = reader["Apellidos"].ToString();
                    client.Nombres = reader["Nombres"].ToString();
                    client.FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]);
                    client.Mail = reader["Mail"].ToString();
                    client.Telefono = reader["Telefono"].ToString();
                    client.Direccion = reader["Direccion"].ToString();
                    client.Estado = Convert.ToBoolean(reader["Estado"]);
                    client.Saldo = Convert.ToDecimal(reader["Saldo"]);

                    lst.Add(client);
                }
                con.Close();
            }
            return lst;
        }

        // Metodo para imprimir los datos en la consola
        public void PrintAllClientes()
        {
            IEnumerable<ClientSql> clientes = GetAllClientes();
            foreach (ClientSql client in clientes)
            {
                Console.WriteLine("Datos SQL Server");
                Console.WriteLine("Codigo: " + client.Codigo);
                Console.WriteLine("Cedula: " + client.Cedula);
                Console.WriteLine("Apellidos: " + client.Apellidos);
                Console.WriteLine("Nombres: " + client.Nombres);
                Console.WriteLine("FechaNacimiento: " + client.FechaNacimiento);
                Console.WriteLine("Mail: " + client.Mail);
                Console.WriteLine("Telefono: " + client.Telefono);
                Console.WriteLine("Direccion: " + client.Direccion);
                Console.WriteLine("Estado: " + client.Estado);
                Console.WriteLine("Saldo: " + client.Saldo);
                Console.WriteLine("----------------------------");
            }
        }

        // Método para verificar si un cliente ya existe por cédula
        public bool ClienteExists(string cedula)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Clientes WHERE Cedula = @Cedula";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Cedula", cedula);

                con.Open();
                int count = (int)cmd.ExecuteScalar();
                con.Close();

                return count > 0;
            }
        }


        //Método para añadir cliente
        public void AddCliente(ClientSql client)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Aquí se está creando un comando SQL para ejecutar el procedimiento almacenado "cliente_Insert"
                SqlCommand cmd = new SqlCommand("cliente_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
            
                cmd.Parameters.AddWithValue("@Cedula", client.Cedula);
                cmd.Parameters.AddWithValue("@Apellidos", client.Apellidos);
                cmd.Parameters.AddWithValue("@Nombres", client.Nombres);
                cmd.Parameters.AddWithValue("@FechaNacimiento", client.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Mail", client.Mail);
                cmd.Parameters.AddWithValue("@Telefono", client.Telefono);
                cmd.Parameters.AddWithValue("@Direccion", client.Direccion);
                cmd.Parameters.AddWithValue("@Estado", client.Estado);
                cmd.Parameters.AddWithValue("@Saldo", client.Saldo);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }



        //Método para obtener cliente por ID
        public ClientSql GetClienteById(int id)
        {
            ClientSql client = new ClientSql();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("cliente_SelectByID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Codigo", id);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    client.Codigo = Convert.ToInt32(reader["Codigo"]);
                    client.Cedula = reader["Cedula"].ToString();
                    client.Apellidos = reader["Apellidos"].ToString();
                    client.Nombres = reader["Nombres"].ToString();
                    client.FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]);
                    client.Mail = reader["Mail"].ToString();
                    client.Telefono = reader["Telefono"].ToString();
                    client.Direccion = reader["Direccion"].ToString();
                    client.Estado = Convert.ToBoolean(reader["Estado"]);
                    client.Saldo = Convert.ToDecimal(reader["Saldo"]);

                }
                con.Close();
            }
            return client;
        }

        // Método para obtener cliente por cédula
        public ClientSql GetClienteByCedula(string cedula)
        {
            ClientSql client = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("select * from Clientes where Cedula = @cedula", con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Cedula", cedula);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    client = new ClientSql
                    {
                        Codigo = Convert.ToInt32(reader["Codigo"]),
                        Cedula = reader["Cedula"].ToString(),
                        Apellidos = reader["Apellidos"].ToString(),
                        Nombres = reader["Nombres"].ToString(),
                        FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                        Mail = reader["Mail"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        Direccion = reader["Direccion"].ToString(),
                        Estado = Convert.ToBoolean(reader["Estado"]),
                        Saldo = Convert.ToDecimal(reader["Saldo"])
                    };
                }
                con.Close();
            }
            return client;
        }




        //Método para actualizar cliente
        public void UpdateCliente(ClientSql client)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("cliente_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;
               
                cmd.Parameters.AddWithValue("@Codigo", client.Codigo);
                cmd.Parameters.AddWithValue("@Cedula", client.Cedula);
                cmd.Parameters.AddWithValue("@Apellidos", client.Apellidos);
                cmd.Parameters.AddWithValue("@Nombres", client.Nombres);
                cmd.Parameters.AddWithValue("@FechaNacimiento", client.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Mail", client.Mail);
                cmd.Parameters.AddWithValue("@Telefono", client.Telefono);
                cmd.Parameters.AddWithValue("@Direccion", client.Direccion);
                cmd.Parameters.AddWithValue("@Estado", client.Estado);
                cmd.Parameters.AddWithValue("@Saldo", client.Saldo);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


        //Método para eliminar cliente
        public void DeleteCliente(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("cliente_Delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@Codigo", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteClienteByCedula(string cedula)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Clientes WHERE Cedula = @cedula", con);
                cmd.CommandType = CommandType.Text; 

                cmd.Parameters.AddWithValue("@cedula", cedula); 

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


    }
}
