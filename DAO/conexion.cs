namespace Api_brazaletes.DAO
{
    public class conexion
    {
        public string ObtenerConexion()
        {
            IConfiguration configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();

            string connectionString = configuration.GetConnectionString("conexion");

            return connectionString;

            // Aquí puedes usar la cadena de conexión en tu lógica de aplicación.
        }
    }
}
