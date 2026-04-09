namespace FitAgenda.Domain.Models;

public class Aula : Entity
{
    protected Aula()
    {
    }

    public Aula(Guid CodigoAula, DateTime dataHora, int capacidadeMaxima)
    {
        DefinirTipoAula(CodigoAula);
        AtualizarDataHora(dataHora);
        DefinirCapacidadeMaxima(capacidadeMaxima);
        Ativar();
    }

    public Guid CodigoAula { get; private set; }
    public TipoAula TipoAula { get; private set; } = null!;
    public DateTime DataHora { get; private set; }
    public int CapacidadeMaxima { get; private set; }
    public bool Ativa { get; private set; }
    public ICollection<Agendamento> Agendamentos { get; private set; } = new List<Agendamento>();

    public void DefinirTipoAula(Guid tipoAulaId)
    {
        CodigoAula = tipoAulaId;
    }

    public void AtualizarDataHora(DateTime dataHora)
    {
        DataHora = dataHora;
    }

    public void DefinirCapacidadeMaxima(int capacidadeMaxima)
    {
        CapacidadeMaxima = capacidadeMaxima;
    }

    public void Ativar()
    {
        Ativa = true;
    }

    public void Inativar()
    {
        Ativa = false;
    }
}
