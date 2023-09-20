namespace WebApiNet6CursoUdemy
{
    public class ConexionBaseDatos
    {
        private string _cadenaConexionSql;
        public string CadenaConexionSQL { get => _cadenaConexionSql;}

        public ConexionBaseDatos(string cadenaConexionSql)
        {
            _cadenaConexionSql= cadenaConexionSql;
        }
    }
}
