using farmed.Models;
using farmed.Repositories;

namespace farmed.Services;

    public class MedicamentoService : IMedicamentoService
    {
        private readonly IMedicamentoRepository _repo;
        public MedicamentoService(IMedicamentoRepository repo)
        {
            _repo = repo;
        }
        public Task<IEnumerable<Medicamento>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Medicamento> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<Medicamento> CreateAsync(Medicamento medicamento) => _repo.AddAsync(medicamento);
        public Task<Medicamento> UpdateAsync(Medicamento medicamento) => _repo.UpdateAsync(medicamento);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }

