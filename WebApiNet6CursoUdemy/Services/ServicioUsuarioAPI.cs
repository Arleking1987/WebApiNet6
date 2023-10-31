using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace WebApiNet6CursoUdemy.Services
{
    public class ServicioUsuarioAPI : IServicioUsuarioAPI
    {
        private string CadenaConexion;
        private readonly ILogger<ServicioUsuarioAPI> _logger;

        public ServicioUsuarioAPI(ConexionBaseDatos conexion, ILogger<ServicioUsuarioAPI> logger)
        {
            _logger = logger;
            CadenaConexion = conexion.CadenaConexionSQL;

        }

        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);
        }
        public async Task<UsuarioAPI> DameUsuario(LoginAPI login)
        {
            SqlConnection sqlConnection = conexion();
            UsuarioAPI usuarioAPI = null;
            try
            {
                sqlConnection.Open();
                var param = new DynamicParameters();
                param.Add("@UsuarioAPI", login.UsuarioAPI, DbType.String, ParameterDirection.Input, 500);
                param.Add("@PassAPI", login.PassAPI, DbType.String, ParameterDirection.Input, 500);
                usuarioAPI = await sqlConnection.QueryFirstOrDefaultAsync<UsuarioAPI>("UsuarioAPIObtener", param, commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {

                _logger.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al obtener datos del usuario de login " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return usuarioAPI;
        }
    }
}
