namespace WebApiNet6CursoUdemy.Services
{
    public class ServicioEmpleado : IServicioEmpleado
    {
        private readonly List<Empleado> ListaEmpleados = new()
        {
            new Empleado{Id=1, Nombre="Juan", CodEmpleado="A001", Email="mail1@mail.com", Edad=45, FechaAlta=DateTime.Now},
            new Empleado{Id=2, Nombre="Pedro", CodEmpleado="A010", Email="mail2@mail.com", Edad=45, FechaAlta=DateTime.Now},
            new Empleado{Id=3, Nombre="Manolo", CodEmpleado="B017", Email="mail3@mail.com", Edad=45, FechaAlta=DateTime.Now},
            new Empleado{Id=4, Nombre="Ana", CodEmpleado="A071", Email="mail@mai4l.com", Edad=45, FechaAlta=DateTime.Now}
        };

        public void BajaEmpleado(string codEmpleado)
        {
            int posicion = ListaEmpleados.FindIndex(existeEmpleado => existeEmpleado.CodEmpleado == codEmpleado);
            if (posicion != -1)
               ListaEmpleados.RemoveAt(posicion);
        }

        public Empleado DameEmpleado(string codEmpleado)
        {
            return ListaEmpleados.Where(e=>e.CodEmpleado == codEmpleado).SingleOrDefault();
        }

        public IEnumerable<Empleado> DameEmpleados()
        {
            return ListaEmpleados;
        }

        public void ModificarEmpleado(Empleado empleado)
        {
            int posicion = ListaEmpleados.FindIndex(existeEmpleado => existeEmpleado.Id == empleado.Id);
            if (posicion != -1)
            {
                ListaEmpleados[posicion] = empleado;
            }
        }

        public void NuevoEmpleado(Empleado empleado)
        {
            ListaEmpleados.Add(empleado);
        }

    }
}
