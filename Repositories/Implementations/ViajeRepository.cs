using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViajesAPI.Models;
using ViajesAPI.Repositories.Interfaces;

namespace ViajesAPI.Repositories.Implementations
{
    public class ViajeRepository : IViajesRepository
    {
        private readonly ViajesContext _context ;

        public ViajeRepository(ViajesContext context)
        {
            _context = context;
        }

        public async Task<List<Viaje>> GetAll(int PrecioMinimo)
        {
             return await _context.Viajes.Where(v => v.PrecioTotal > PrecioMinimo).ToListAsync();
        }

        public async Task<Viaje?> GetByEstado(string estado)
        {
            return await _context.Viajes.Include(v => v.ViajeDetalles).ThenInclude(vd => vd.Excursion).FirstOrDefaultAsync(v => v.Estado == estado);
        }

        public async Task<List<Excursion>> GetByIdExcursion(List<int> ids)
        {
            return await _context.Excursiones.Where(e => ids.Contains(e.Id)).ToListAsync();
        }

        public async Task<bool> UpdateDate(int id, DateTime fechaInicio)
        {
            Viaje? viaje = await _context.Viajes.FirstOrDefaultAsync(v => v.Id == id);
            if (viaje == null) { return false; }
            viaje.FechaInicio=fechaInicio;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Viaje?> GetById(int id)
        {
            return await _context.Viajes.Include(v => v.ViajeDetalles).ThenInclude(vd => vd.Excursion).FirstOrDefaultAsync(v => v.Id == id);
        }
    }
}
