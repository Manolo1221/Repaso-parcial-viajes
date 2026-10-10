using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViajesAPI.Models;

namespace ViajesAPI.Repositories.Interfaces
{
    public interface IViajesRepository
    {
        public Task<List<Viaje>> GetAll(int PrecioMinimo);
        public Task<Viaje?> GetByEstado(string estado);
        public Task<Viaje?> GetById(int id);
        public Task<List<Excursion>> GetByIdExcursion(List<int> ids);
        public Task<bool> UpdateDate(int id, DateTime fechaInicio);
    }
}
