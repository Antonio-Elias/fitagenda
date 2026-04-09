using FitAgenda.Domain.Models;

namespace FitAgenda.Domain.Interfaces;

public interface ITipoAulaService
{
    void Criar(TipoAula tipoAula, IEnumerable<TipoAula> tiposExistentes);
    void Atualizar(TipoAula tipoAula, IEnumerable<TipoAula> tiposExistentes);
}
