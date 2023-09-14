using WebApiNet6CursoUdemy.DTO;

namespace WebApiNet6CursoUdemy
{
    public static class Utilidades
    {
        //Ponemos la palabra reservada this al parametro de entrada del método porque la hace como una extensión de la clase, lo que nos permite llamarla desde otra clase
        // sin problema y sin necesidad de instanciar o hacer la inyeccion de dependencias
        public static EmpleadoDTO convertirDTO(this Empleado empleado)
        {
            if (empleado != null)
            {
                return new EmpleadoDTO
                {
                    Nombre = empleado.Nombre,
                    CodEmpleado = empleado.CodEmpleado,
                    Email = empleado.Email,
                    Edad = empleado.Edad
                };
            }

            return null;
        }
    }
}
