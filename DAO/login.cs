using Api_brazaletes.Controllers;
using Api_brazaletes.Models;
using System.Data.SqlClient;
using System.Data;

namespace Api_brazaletes.DAO
{
    public class login
    {
        public   List<Usuarios> Login(Usuarios Lectura)
        {
            conexion conexion = new conexion();
            string cadenaconexion = conexion.ObtenerConexion();
            List<Usuarios> rsp = new List<Usuarios>();

            using (SqlConnection connection = new SqlConnection(cadenaconexion))
            {
                
                // Nombre del procedimiento almacenado
                string storedProcedureName = "Login";

                using (SqlCommand cmd = new SqlCommand(storedProcedureName, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros del procedimiento almacenado

                    cmd.Parameters.AddWithValue("@operador", Lectura.operador);
                    cmd.Parameters.AddWithValue("@password", Lectura.clave);

                    connection.Open();

                    using (SqlDataReader reader =  cmd.ExecuteReader())
                    {
                        while ( reader.Read())
                        {
                            var usuario = new Usuarios
                            {
                                id = reader[0].ToString(),
                                operador = reader[1].ToString()
                            };

                            rsp.Add(usuario);
                        }
                    }

                    connection.Close();

                }
            }

            return rsp;
        }
    }
}
