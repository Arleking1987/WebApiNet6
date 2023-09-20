namespace WebApiNet6CursoUdemy
{
    public class Empleado
    {
        //La propiedad init indica que el valor que se le asigne solo se puede realizar una vez cuando se inicialice el objeto
        public int Id { get; init; }
        public string Nombre { get; set; }
        public string CodEmpleado { get; set; }
        public string Email { get; set; }
        public int Edad { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
