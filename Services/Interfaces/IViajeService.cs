using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViajesAPI.DTOs;
using ViajesAPI.Models;

namespace ViajesAPI.Services.Interfaces
{
    public interface IViajeService
    {
        public Task<List<Viaje>> ObtenerTodos();
        public Task<Viaje?> ObtenerPorEstado(ObtenerPorEstadoDto dto);
        public Task<bool> ActualizarFecha(int id, ActualizarFechaDto dto);
    }
}
