namespace WebApiNet6CursoUdemy.Services
{
    public interface IServicioUsuarioAPI
    {
        Task<UsuarioAPI> DameUsuario(LoginAPI login);
    }
}
