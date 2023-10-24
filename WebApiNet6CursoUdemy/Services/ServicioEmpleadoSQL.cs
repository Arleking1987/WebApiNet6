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

        public async Task BajaEmpleado(string codEmpleado)
        {
            SqlConnection sqlConexion = conexion();
          

            try
            {
                sqlConexion.Open();
                var param = new DynamicParameters();

                param.Add("@CodEmpleado", codEmpleado, DbType.String, ParameterDirection.Input, 4);
                await sqlConexion.ExecuteScalarAsync("EmpleadoBorrar", param, commandType: CommandType.StoredProcedure);


            }
            catch (Exception ex)
            {

                throw new Exception("Se produjo un error al borrar un empleado " + ex.Message);
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
        }

        public async Task<Empleado> DameEmpleado(string codEmpleado)
        {
            SqlConnection sqlConexion = conexion();
            Empleado empleado = null;

            try
            {
                sqlConexion.Open();
                var param = new DynamicParameters();
              
                param.Add("@CodEmpleado", codEmpleado, DbType.String, ParameterDirection.Input, 4);
                empleado = await sqlConexion.QueryFirstOrDefaultAsync<Empleado>("EmpleadoObtener", param, commandType: CommandType.StoredProcedure);


            }
            catch (Exception ex)
            {

                throw new Exception("Se produjo un error al obtener un empleado " + ex.Message);
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return empleado; 
        }

        public async Task<IEnumerable<Empleado>> DameEmpleados()
        {
            SqlConnection sqlConexion = conexion();
            List <Empleado> empleados = new List<Empleado>();

            try
            {
                sqlConexion.Open();
                
                var resultado = await sqlConexion.QueryAsync<Empleado>("EmpleadoObtener", commandType: CommandType.StoredProcedure);
                empleados = resultado.ToList();


            }
            catch (Exception ex)
            {

                throw new Exception("Se produjo un error al obtener los empleados " + ex.Message);
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return empleados;
        }

        public async Task ModificarEmpleado(Empleado empleado)
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
                await sqlConexion.ExecuteScalarAsync("EmpleadoModificar", param, commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {

                throw new Exception("Se produjo un error al modificar empleado " + ex.Message);
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
        }

        public async Task NuevoEmpleado(Empleado empleado)
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
                await sqlConexion.ExecuteScalarAsync("EmpleadoAlta", param, commandType: CommandType.StoredProcedure);

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
