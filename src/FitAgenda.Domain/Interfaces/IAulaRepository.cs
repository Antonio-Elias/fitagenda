using FitAgenda.Domain.Models;

namespace FitAgenda.Domain.Interfaces;

public interface IAulaRepository : IRepository<Aula>
{
    Task<Aula?> ObterAulaComTipo(Guid codigoAula);
    Task<List<Aula>> ObterAulasComTipo();
}
