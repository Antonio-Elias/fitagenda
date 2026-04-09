using FitAgenda.Domain.Interfaces;
using FitAgenda.Domain.Models;
using FitAgenda.Domain.Validations;


namespace FitAgenda.Domain.Services;

public class AulaService : BaseService, IAulaService
{
    public AulaService(INotificador notificador)
        : base(notificador)
    {
    }

    public void Criar(Aula aula, TipoAula tipoAula)
    {
        ExecutarValidacao(new AulaValidation(), aula);
        ValidarTipoAulaAtivo(tipoAula);
    }

    private void ValidarTipoAulaAtivo(TipoAula tipoAula)
    {
        if (!tipoAula.Ativo)
            Notificar("Nao e permitido cadastrar aula para um tipo de aula inativo.");
    }
}
