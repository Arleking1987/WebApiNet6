using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiNet6CursoUdemy.DTO;
using WebApiNet6CursoUdemy.Services;

namespace WebApiNet6CursoUdemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmpleadoController : ControllerBase
    {
        private readonly IServicioEmpleadoSQL _servicioEmpleado;
        public EmpleadoController(IServicioEmpleadoSQL servicioEmpleado)
        {
            _servicioEmpleado = servicioEmpleado;
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<EmpleadoDTO>> DameEmpleados()
        {
            //Utilizamos LINQ por medio de "Select" para utilizar el metodo de convertir deteo para mapear el objeto Empleado a EmpleadoDTO y arrojar los resultados
            //Como una lista de EmpleadoDTO y no de Empleado
            var listaEmpleados = (await _servicioEmpleado.DameEmpleados()).Select(empleado => empleado.convertirDTO());
            return listaEmpleados;
        }

        [HttpGet("{codEmpleado}")]
        [Authorize]
        public async Task<ActionResult<EmpleadoDTO>> DameEmpleado(string codEmpleado)
        {
            EmpleadoDTO empleado = (await _servicioEmpleado.DameEmpleado(codEmpleado)).convertirDTO();
            if(empleado is null)
            {
                return NotFound("Empleado no encontrado para el código solicitado");
            }
            return empleado;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<EmpleadoDTO>> NuevoEmpleado(EmpleadoDTO empleadoDTO)
        {
            Empleado empleado = new Empleado
            {
                
                CodEmpleado = empleadoDTO.CodEmpleado,
                Nombre = empleadoDTO.Nombre,
                Email = empleadoDTO.Email,
                Edad = empleadoDTO.Edad,
                FechaAlta = DateTime.Now
            };

           await _servicioEmpleado.NuevoEmpleado(empleado);
            return empleado.convertirDTO();
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<EmpleadoDTO>> ModificarEmpleado(EmpleadoDTO empleado)
        {
            Empleado empleadoAux = await _servicioEmpleado.DameEmpleado(empleado.CodEmpleado);

            if (empleadoAux is null)
            {
                return NotFound("Empleado no encontrado para el código solicitado");
            }

            empleadoAux.CodEmpleado = empleado.CodEmpleado;
            empleadoAux.Nombre = empleado.Nombre;
            empleadoAux.Email = empleado.Email;
            empleadoAux.Edad = empleado.Edad;

           await _servicioEmpleado.ModificarEmpleado(empleadoAux);
            return empleado;
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> BorrarEmpleado(string codEmpleado)
        {
            Empleado empleadoAux = await _servicioEmpleado.DameEmpleado(codEmpleado);

            if (empleadoAux is null)
            {
                return NotFound();
            }

           await _servicioEmpleado.BajaEmpleado(codEmpleado);

            return Ok();
        }
    }
}
