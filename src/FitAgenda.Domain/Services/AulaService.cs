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
        ValidarEntidade(aula);

        if (TemNotificacao())
            return;

        ValidarTipoAulaAtivo(tipoAula);
    }

    public void Atualizar(Aula aula, TipoAula tipoAula)
    {
        ValidarEntidade(aula);

        if (TemNotificacao())
            return;

        ValidarTipoAulaAtivo(tipoAula);
    }

    private void ValidarEntidade(Aula aula)
    {
        ExecutarValidacao(new AulaValidation(), aula);
    }

    private void ValidarTipoAulaAtivo(TipoAula tipoAula)
    {
        if (!tipoAula.Ativo)
            Notificar("Nao e permitido cadastrar aula para um tipo de aula inativo.");
    }
}
