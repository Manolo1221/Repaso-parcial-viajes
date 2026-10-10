using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViajesAPI.DTOs;
using ViajesAPI.Models;
using ViajesAPI.Repositories.Interfaces;
using ViajesAPI.Services.Interfaces;

namespace ViajesAPI.Services.Implementations
{
    public class ViajeService : IViajeService
    {
        public readonly IViajesRepository _repository;
        public static int minimo = 100000;
        //public static string[] estadosValidos = { "Pendiente", "Confirmado", "Cancelado" };

        public ViajeService(IViajesRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ActualizarFecha(int id, ActualizarFechaDto dto)
        {
            Viaje? viaje = await _repository.GetById(id);
            if (viaje == null) { throw new ArgumentException("El viaje no existe"); }
            if(dto.FechaInicio.Date < DateTime.Today) { throw new ArgumentException("La fecha de incio no puede ser anterior a hoy"); }
            if(dto.FechaInicio.Date >= viaje.FechaFin.Date) { throw new ArgumentException("La fecha de incio no puede posterior a la de final"); }
            return await _repository.UpdateDate(id, dto.FechaInicio);
        }

        public async Task<Viaje?> ObtenerPorEstado(ObtenerPorEstadoDto dto)
        {
            if (dto.Estado != "Pendiente" && dto.Estado != "Confirmado" && dto.Estado != "Cancelado") { throw new ArgumentException("Estado no valido"); }
            return await _repository.GetByEstado(dto.Estado);
        }

        public async Task<List<Viaje>> ObtenerTodos()
        {
            return await _repository.GetAll(minimo);
        }
    }
}
