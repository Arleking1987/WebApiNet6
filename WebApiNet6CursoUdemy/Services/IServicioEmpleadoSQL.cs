namespace WebApiNet6CursoUdemy.Services
{
    public interface IServicioEmpleadoSQL
    {
        public IEnumerable<Empleado> DameEmpleados();

        public Empleado DameEmpleado(string codEmpleado);
        public void NuevoEmpleado(Empleado empleado);

        public void ModificarEmpleado(Empleado empleado);

        public void BajaEmpleado(string codEmpleado);
    }
}
