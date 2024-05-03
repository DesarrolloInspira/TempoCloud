using Api_brazaletes.Models;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Api_brazaletes.DAO
{
    public class operacion
    {

        public List<Respuesta> Insertar(Operacion Lectura)
        {
            conexion conexion = new conexion();
            string cadenaconexion = conexion.ObtenerConexion();
            List<Respuesta> rsp = new List<Respuesta>();
            try
            {

           
            // valida Brazaletes
            if (Lectura.Codigo_Brazalete.Length > 0 && Lectura.Codigo_Boleto == "")
            {
                using (SqlConnection connection = new SqlConnection(cadenaconexion))
                {
                    using (SqlCommand cmd = new SqlCommand("validarBrazalete", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ValorAValidar", Lectura.Codigo_Brazalete);
                        cmd.Parameters.AddWithValue("@zona", Lectura.ID_Zona);

                        connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj1 = new Respuesta
                                {
                                    ID_Estatus = "-1",
                                    Codigo_Boleto = reader["Codigo_Boleto"].ToString(),
                                    Codigo_Brazalete = reader["Codigo_Brazalete"].ToString()
                                };

                                rsp.Add(obj1);
                                connection.Close();

                                return rsp;
                            }

                                var objx = new Respuesta
                                {
                                    ID_Estatus = "-2",
                                    Codigo_Boleto = "",
                                    Codigo_Brazalete = ""
                                };

                                rsp.Add(objx);
                                connection.Close();

                                return rsp;

                            }



                    }
                }
            }

            // FIN valida Brazaletes
            


            int rsp2 = 0; 

            using (SqlConnection connection = new SqlConnection(cadenaconexion))
            {
                connection.Open();
                string storedProcedureName = "InsertarOperacion";
                using (SqlCommand cmd = new SqlCommand(storedProcedureName, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_Operador", Lectura.ID_Operador);
                    cmd.Parameters.AddWithValue("@ID_Zona", Lectura.ID_Zona);
                    cmd.Parameters.AddWithValue("@Codigo_Boleto", Lectura.Codigo_Boleto);
                    cmd.Parameters.AddWithValue("@Codigo_Brazalete", Lectura.Codigo_Brazalete);
                   rsp2 =  cmd.ExecuteNonQuery();
                }

                var obj2 = new Respuesta
                {
                    ID_Estatus = rsp2.ToString(),
                    Codigo_Boleto = "",
                    Codigo_Brazalete = ""
                };

                rsp.Add(obj2);
                connection.Close();

                return rsp;

            }
            }
            catch (Exception)
            {
                return rsp;

            }

        }



        public bool ValidarRegistroExistente(string valorAValidar, int zona)
        {
            conexion con = new conexion();
            bool registroExiste = false;
            string connectionString = con.ObtenerConexion();


            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                // Abrir la conexión
                conexion.Open();

                // Configurar y ejecutar el comando del procedimiento almacenado
                using (SqlCommand comando = new SqlCommand("validarBoleto", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros de entrada
              
                    comando.Parameters.AddWithValue("@ValorAValidar", valorAValidar);
                    comando.Parameters.AddWithValue("@zona", zona);

                    // Agregar parámetro de salida
                    SqlParameter parametroSalida = new SqlParameter("@RegistroExiste", SqlDbType.Bit);
                    parametroSalida.Direction = ParameterDirection.Output;
                    comando.Parameters.Add(parametroSalida);

                    // Ejecutar el procedimiento almacenado
                    comando.ExecuteNonQuery();
                    conexion.Close();
                    // Obtener el valor del parámetro de salida
                    registroExiste = (bool)parametroSalida.Value;
                }
            }

            return registroExiste;
        }


    }
}
