using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiNet6CursoUdemy.DTO;
using WebApiNet6CursoUdemy.Services;

namespace WebApiNet6CursoUdemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IServicioEmpleadoSQL _servicioEmpleado;
        public EmpleadoController(IServicioEmpleadoSQL servicioEmpleado)
        {
            _servicioEmpleado = servicioEmpleado;
        }

        [HttpGet]
        public IEnumerable<EmpleadoDTO> DameEmpleados()
        {
            //Utilizamos LINQ por medio de "Select" para utilizar el metodo de convertir deteo para mapear el objeto Empleado a EmpleadoDTO y arrojar los resultados
            //Como una lista de EmpleadoDTO y no de Empleado
            IEnumerable<EmpleadoDTO> listaEmpleados = _servicioEmpleado.DameEmpleados().Select(empleado => empleado.convertirDTO());
            return listaEmpleados;
        }

        [HttpGet("{codEmpleado}")]
        public ActionResult<EmpleadoDTO> DameEmpleado(string codEmpleado)
        {
            EmpleadoDTO empleado = _servicioEmpleado.DameEmpleado(codEmpleado).convertirDTO();
            if(empleado is null)
            {
                return NotFound(empleado);
            }
            return empleado;
        }

        [HttpPost]
        public ActionResult<EmpleadoDTO> NuevoEmpleado(EmpleadoDTO empleadoDTO)
        {
            Empleado empleado = new Empleado
            {
                
                CodEmpleado = empleadoDTO.CodEmpleado,
                Nombre = empleadoDTO.Nombre,
                Email = empleadoDTO.Email,
                Edad = empleadoDTO.Edad,
                FechaAlta = DateTime.Now
            };

            _servicioEmpleado.NuevoEmpleado(empleado);
            return empleado.convertirDTO();
        }

        [HttpPut]
        public ActionResult<EmpleadoDTO> ModificarEmpleado(EmpleadoDTO empleado)
        {
            Empleado empleadoAux = _servicioEmpleado.DameEmpleado(empleado.CodEmpleado);

            if (empleadoAux is null)
            {
                return NotFound();
            }

            empleadoAux.CodEmpleado = empleado.CodEmpleado;
            empleadoAux.Nombre = empleado.Nombre;
            empleadoAux.Email = empleado.Email;
            empleadoAux.Edad = empleado.Edad;

            _servicioEmpleado.ModificarEmpleado(empleadoAux);
            return empleado;
        }

        [HttpDelete]
        public ActionResult BorrarEmpleado(string codEmpleado)
        {
            Empleado empleadoAux = _servicioEmpleado.DameEmpleado(codEmpleado);

            if (empleadoAux is null)
            {
                return NotFound();
            }

            _servicioEmpleado.BajaEmpleado(codEmpleado);

            return Ok();
        }
    }
}
