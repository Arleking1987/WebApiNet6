namespace WebApiNet6CursoUdemy.Services
{
    public interface IServicioEmpleadoSQL
    {
        public Task<IEnumerable<Empleado>> DameEmpleados();

        public Task<Empleado> DameEmpleado(string codEmpleado);
        public Task NuevoEmpleado(Empleado empleado);

        public Task ModificarEmpleado(Empleado empleado);

        public Task BajaEmpleado(string codEmpleado);
    }
}
