using Laboratorio1_3P.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Laboratorio1_3P.Data
{
    public class ClientPostgresDataAccessLayer
    {
        // Realizar la conexión a la base de datos, connection string
        string connectionString = "Host=localhost;Port=5433;Database=dbproductos;Username=postgres;Password=170311";

        public IEnumerable<ClientePostgres> GetAllClientes()
        {
            List<ClientePostgres> lst = new List<ClientePostgres>();

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                using (var transaction = con.BeginTransaction())
                {
                    using (var cmd = new NpgsqlCommand("cliente_selectall", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Transaction = transaction;

                        // Parámetro de salida para el cursor
                        var cursorParam = new NpgsqlParameter("ref_cursor", NpgsqlTypes.NpgsqlDbType.Refcursor)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(cursorParam);

                        // Ejecuta el procedimiento almacenado para abrir el cursor
                        cmd.ExecuteNonQuery();

                        // Ahora obtenemos el nombre del cursor
                        string cursorName = (string)cursorParam.Value;

                        // Ejecuta el comando para recuperar los datos del cursor
                        using (var fetchCmd = new NpgsqlCommand($"FETCH ALL IN \"{cursorName}\";", con, transaction))
                        using (var reader = fetchCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientePostgres client = new ClientePostgres
                                {
                                    codigo = Convert.ToInt32(reader["codigo"]),
                                    cedula = reader["cedula"].ToString(),
                                    apellidos = reader["apellidos"].ToString(),
                                    nombres = reader["nombres"].ToString(),
                                    fechanacimiento = Convert.ToDateTime(reader["fechanacimiento"]),
                                    mail = reader["mail"].ToString(),
                                    telefono = reader["telefono"].ToString(),
                                    direccion = reader["direccion"].ToString(),
                                    estado = Convert.ToBoolean(reader["estado"])
                                };

                                lst.Add(client);
                            }
                        }
                    }

                    transaction.Commit();
                }

                con.Close();
            }

            return lst;
        }

        public ClientePostgres GetClienteById(int id)
        {
            ClientePostgres client = null;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM clientes WHERE codigo = @codigo", con);
                cmd.Parameters.AddWithValue("@codigo", id);
                cmd.CommandType = CommandType.Text;

                con.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    client = new ClientePostgres
                    {
                        codigo = Convert.ToInt32(reader["codigo"]),
                        cedula = reader["cedula"].ToString(),
                        apellidos = reader["apellidos"].ToString(),
                        nombres = reader["nombres"].ToString(),
                        fechanacimiento = Convert.ToDateTime(reader["fechanacimiento"]),
                        mail = reader["mail"].ToString(),
                        telefono = reader["telefono"].ToString(),
                        direccion = reader["direccion"].ToString(),
                        estado = Convert.ToBoolean(reader["estado"])
                    };
                }
                con.Close();
            }
            return client;
        }

        public void InsertCliente(ClientePostgres client)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                using (var cmd = new NpgsqlCommand("cliente_create", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("p_cedula", client.cedula);
                    cmd.Parameters.AddWithValue("p_apellidos", client.apellidos);
                    cmd.Parameters.AddWithValue("p_nombres", client.nombres);
                    cmd.Parameters.AddWithValue("p_fechanacimiento", client.fechanacimiento);
                    cmd.Parameters.AddWithValue("p_mail", client.mail);
                    cmd.Parameters.AddWithValue("p_telefono", client.telefono);
                    cmd.Parameters.AddWithValue("p_direccion", client.direccion);
                    cmd.Parameters.AddWithValue("p_estado", client.estado);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }


        public void UpdateCliente(ClientePostgres client)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                string query = "UPDATE clientes SET cedula = @cedula, apellidos = @apellidos, nombres = @nombres, " +
                               "fechanacimiento = @fechanacimiento, mail = @mail, telefono = @telefono, direccion = @direccion, estado = @estado " +
                               "WHERE codigo = @codigo";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);

                cmd.Parameters.AddWithValue("@codigo", client.codigo);
                cmd.Parameters.AddWithValue("@cedula", client.cedula);
                cmd.Parameters.AddWithValue("@apellidos", client.apellidos);
                cmd.Parameters.AddWithValue("@nombres", client.nombres);
                cmd.Parameters.AddWithValue("@fechanacimiento", client.fechanacimiento);
                cmd.Parameters.AddWithValue("@mail", client.mail);
                cmd.Parameters.AddWithValue("@telefono", client.telefono);
                cmd.Parameters.AddWithValue("@direccion", client.direccion);
                cmd.Parameters.AddWithValue("@estado", client.estado);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteCliente(int id)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM clientes WHERE codigo = @codigo", con);
                cmd.Parameters.AddWithValue("@codigo", id);
                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // Método para imprimir los datos en la consola
        public void PrintAllClientes()
        {
            IEnumerable<ClientePostgres> clientes = GetAllClientes();
            foreach (ClientePostgres client in clientes)
            {
                Console.WriteLine("Datos Postgres");
                Console.WriteLine("Codigo: " + client.codigo);
                Console.WriteLine("Cedula: " + client.cedula);
                Console.WriteLine("Apellidos: " + client.apellidos);
                Console.WriteLine("Nombres: " + client.nombres);
                Console.WriteLine("FechaNacimiento: " + client.fechanacimiento);
                Console.WriteLine("Mail: " + client.mail);
                Console.WriteLine("Telefono: " + client.telefono);
                Console.WriteLine("Direccion: " + client.direccion);
                Console.WriteLine("Estado: " + client.estado);
                Console.WriteLine("----------------------------");
            }
        }
    }
}
