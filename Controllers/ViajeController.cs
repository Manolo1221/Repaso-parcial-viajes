using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViajesAPI.DTOs;
using ViajesAPI.Models;
using ViajesAPI.Services.Interfaces;

namespace ViajesAPI.Controllers
{
    [Route("api/viajes")]
    [ApiController]
    public class ViajeController : ControllerBase
    {
        private readonly IViajeService _service;

        public ViajeController(IViajeService service)
        {
            _service = service;
        }
        private IActionResult ManejarError(Exception ex)
        {
            return ex switch
            {
                ArgumentException => BadRequest(ex.Message),
                ConflictException => Conflict(ex.Message),
                DbUpdateException => Conflict("No se pudo guardar por conflicto en los datos"),
                DbException => StatusCode(503, "Error al acceder en la BD"),
                _ => StatusCode(500, "Error interno del servidor")
            };
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var viajes = await _service.ObtenerTodos();
                return Ok(viajes);
            }
            catch (Exception ex)
            {
                return ManejarError(ex);
            }
        }

        [HttpGet("{estado}")]
        public async Task<IActionResult> GetByEstado([FromRoute] string estado)
        {
            
            try
            {
                var dto = new ObtenerPorEstadoDto { Estado = estado };
                Viaje? viaje = await _service.ObtenerPorEstado(dto);
                if (viaje == null) { return NotFound("No hay viajes con ese estado"); };
                return Ok(viaje);
            }
            catch (Exception ex )
            {
                return ManejarError(ex);
            }
        }
        [HttpPut("{id}/fecha")]
        public async Task<IActionResult> UpdateFecha([FromRoute] int id, [FromBody] ActualizarFechaDto dto)
        {
            try
            {
                bool v = await _service.ActualizarFecha(id, dto);
                if (!v) { return NotFound("El id no tiene un viaje"); }
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return ManejarError(ex);
            }
        }

    }
}
