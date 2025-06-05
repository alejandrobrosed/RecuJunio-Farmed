using farmed.Models;

public interface IMedicamentoService
{
    Task<IEnumerable<Medicamento>> GetAllAsync();
    Task<Medicamento> GetByIdAsync(int id);
    Task<Medicamento> CreateAsync(Medicamento medicamento);
    Task<Medicamento> UpdateAsync(Medicamento medicamento);
    Task<bool> DeleteAsync(int id);
}