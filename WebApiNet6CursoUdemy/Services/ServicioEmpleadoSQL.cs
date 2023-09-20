using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace WebApiNet6CursoUdemy.Services
{
    public class ServicioEmpleadoSQL : IServicioEmpleadoSQL
    {
        private string _cadenaConexionSql;

        public ServicioEmpleadoSQL(ConexionBaseDatos cadenaConexionSql)
        {
            _cadenaConexionSql = cadenaConexionSql.CadenaConexionSQL;
        }

        private SqlConnection conexion()
        {
            return new SqlConnection(_cadenaConexionSql);
        }

        public void BajaEmpleado(string codEmpleado)
        {
            throw new NotImplementedException();
        }

        public Empleado DameEmpleado(string codEmpleado)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Empleado> DameEmpleados()
        {
            throw new NotImplementedException();
        }

        public void ModificarEmpleado(Empleado empleado)
        {
            throw new NotImplementedException();
        }

        public void NuevoEmpleado(Empleado empleado)
        {
            SqlConnection sqlConexion = conexion();

            try
            {
                sqlConexion.Open();
                var param = new DynamicParameters();
                param.Add("@Nombre", empleado.Nombre, DbType.String, ParameterDirection.Input, 500);
                param.Add("@CodEmpleado", empleado.CodEmpleado, DbType.String, ParameterDirection.Input, 4);
                param.Add("@Email", empleado.Email, DbType.String, ParameterDirection.Input, 225);
                param.Add("@Edad", empleado.Edad, DbType.Int32, ParameterDirection.Input);
                param.Add("@FechaAlta", empleado.FechaAlta, DbType.DateTime, ParameterDirection.Input);
                sqlConexion.ExecuteScalar("EmpleadoAlta", param, commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {

                throw new Exception("Se produjo un error al dar de alta " + ex.Message);
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }        
        }
    }
}
