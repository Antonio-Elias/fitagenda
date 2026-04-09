using FitAgenda.Domain.Interfaces;
using FitAgenda.Domain.Models;
using FitAgenda.Domain.Validations;

namespace FitAgenda.Domain.Services;

public class TipoAulaService : BaseService, ITipoAulaService
{
    public TipoAulaService(INotificador notificador)
        : base(notificador)
    {
    }

    public void Criar(TipoAula tipoAula, IEnumerable<TipoAula> tiposExistentes)
    {
        ExecutarValidacao(new TipoAulaValidation(), tipoAula);
        ValidarNomeDuplicado(tipoAula.Nome, tiposExistentes);
    }

    public void Atualizar(TipoAula tipoAula, IEnumerable<TipoAula> tiposExistentes)
    {
        ExecutarValidacao(new TipoAulaValidation(), tipoAula);
        ValidarNomeDuplicado(tipoAula.Nome, tiposExistentes, tipoAula.Id);
    }

    private void ValidarNomeDuplicado(string nome, IEnumerable<TipoAula> tiposExistentes, Guid? idIgnorar = null)
    {
        if (NomeJaExiste(nome, tiposExistentes, idIgnorar))
            Notificar("Ja existe um tipo de aula com esse nome.");
    }

    private static bool NomeJaExiste(string nome, IEnumerable<TipoAula> tiposExistentes, Guid? idIgnorar = null)
    {
        return tiposExistentes.Any(tipoAula =>
            (idIgnorar == null || tipoAula.Id != idIgnorar) &&
            string.Equals(tipoAula.Nome.Trim(), nome.Trim(), StringComparison.OrdinalIgnoreCase));
    }
}
