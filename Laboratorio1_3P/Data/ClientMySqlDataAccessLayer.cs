using Laboratorio1_3P.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Laboratorio1_3P.Data
{
    public class ClientMySqlDataAccessLayer
    {
        // Realizar la conexión a la base de datos, connection string
        string connectionString = "Server=localhost;Database=dbproductos;User ID=root;Password=170311;";

        public IEnumerable<ClientMySql> GetAllClientes()
        {
            List<ClientMySql> lst = new List<ClientMySql>();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("cliente_SelectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ClientMySql client = new ClientMySql
                    {
                        Codigo = Convert.ToInt32(reader["codigo"]),
                        Cedula = reader["cedula"].ToString(),
                        Apellidos = reader["apellidos"].ToString(),
                        Nombres = reader["nombres"].ToString(),
                        FechaNacimiento = Convert.ToDateTime(reader["fechanacimiento"]),
                        Mail = reader["mail"].ToString(),
                        Telefono = reader["telefono"].ToString(),
                        Direccion = reader["direccion"].ToString(),
                        Estado = Convert.ToBoolean(reader["estado"])
                    };

                    lst.Add(client);
                }
                con.Close();
            }
            return lst;
        }

        public ClientMySql GetClienteById(int id)
        {
            ClientMySql client = null;

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("cliente_SelectByID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_id", id);

                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    client = new ClientMySql
                    {
                        Codigo = Convert.ToInt32(reader["codigo"]),
                        Cedula = reader["cedula"].ToString(),
                        Apellidos = reader["apellidos"].ToString(),
                        Nombres = reader["nombres"].ToString(),
                        FechaNacimiento = Convert.ToDateTime(reader["fechanacimiento"]),
                        Mail = reader["mail"].ToString(),
                        Telefono = reader["telefono"].ToString(),
                        Direccion = reader["direccion"].ToString(),
                        Estado = Convert.ToBoolean(reader["estado"])
                    };
                }
                con.Close();
            }
            return client;
        }

        public ClientMySql GetClienteByCedula(string cedula)
        {
            ClientMySql client = null;

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM clientes WHERE cedula = @cedula", con);
                cmd.Parameters.AddWithValue("@cedula", cedula);
                cmd.CommandType = CommandType.Text;

                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    client = new ClientMySql
                    {
                        Codigo = Convert.ToInt32(reader["codigo"]),
                        Cedula = reader["cedula"].ToString(),
                        Apellidos = reader["apellidos"].ToString(),
                        Nombres = reader["nombres"].ToString(),
                        FechaNacimiento = Convert.ToDateTime(reader["fechanacimiento"]),
                        Mail = reader["mail"].ToString(),
                        Telefono = reader["telefono"].ToString(),
                        Direccion = reader["direccion"].ToString(),
                        Estado = Convert.ToBoolean(reader["estado"])
                    };
                }
                con.Close();
            }
            return client;
        }

        public void InsertCliente(ClientMySql client)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("cliente_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@p_cedula", client.Cedula);
                cmd.Parameters.AddWithValue("@p_apellidos", client.Apellidos);
                cmd.Parameters.AddWithValue("@p_nombres", client.Nombres);
                cmd.Parameters.AddWithValue("@p_fechanacimiento", client.FechaNacimiento);
                cmd.Parameters.AddWithValue("@p_mail", client.Mail);
                cmd.Parameters.AddWithValue("@p_telefono", client.Telefono);
                cmd.Parameters.AddWithValue("@p_direccion", client.Direccion);
                cmd.Parameters.AddWithValue("@p_estado", client.Estado);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateCliente(ClientMySql client)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("cliente_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@p_id", client.Codigo);
                cmd.Parameters.AddWithValue("@p_cedula", client.Cedula);
                cmd.Parameters.AddWithValue("@p_apellidos", client.Apellidos);
                cmd.Parameters.AddWithValue("@p_nombres", client.Nombres);
                cmd.Parameters.AddWithValue("@p_fechanacimiento", client.FechaNacimiento);
                cmd.Parameters.AddWithValue("@p_mail", client.Mail);
                cmd.Parameters.AddWithValue("@p_telefono", client.Telefono);
                cmd.Parameters.AddWithValue("@p_direccion", client.Direccion);
                cmd.Parameters.AddWithValue("@p_estado", client.Estado);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteCliente(int id)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("cliente_Delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_id", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // Método para imprimir los datos en la consola
        public void PrintAllClientes()
        {
            IEnumerable<ClientMySql> clientes = GetAllClientes();
            foreach (ClientMySql client in clientes)
            {
                Console.WriteLine("Datos MySQL");
                Console.WriteLine("Codigo: " + client.Codigo);
                Console.WriteLine("Cedula: " + client.Cedula);
                Console.WriteLine("Apellidos: " + client.Apellidos);
                Console.WriteLine("Nombres: " + client.Nombres);
                Console.WriteLine("FechaNacimiento: " + client.FechaNacimiento);
                Console.WriteLine("Mail: " + client.Mail);
                Console.WriteLine("Telefono: " + client.Telefono);
                Console.WriteLine("Direccion: " + client.Direccion);
                Console.WriteLine("Estado: " + client.Estado);
                Console.WriteLine("----------------------------");
            }
        }
    }
}
