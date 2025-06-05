using System.Collections.Generic;
using System.Threading.Tasks;
using farmed.Models;

public interface IMedicamentoRepository
{
    public Task<IEnumerable<Medicamento>> GetAllAsync();
    public Task<Medicamento> GetByIdAsync(int id);
    public Task<Medicamento> AddAsync(Medicamento medicamento);
    public Task<Medicamento> UpdateAsync(Medicamento medicamento);
    public Task<bool> DeleteAsync(int id);
}