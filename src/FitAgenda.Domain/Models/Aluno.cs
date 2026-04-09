

using FitAgenda.Domain.Enums;

namespace FitAgenda.Domain.Models;

public class Aluno : Entity
{
    protected Aluno()
    {
    }

    public Aluno(string nome, TipoPlano tipoPlano)
    {
        AtualizarNome(nome);
        AlterarPlano(tipoPlano);
        Ativar();
    }

    public string Nome { get; private set; } = string.Empty;
    public TipoPlano TipoPlano { get; private set; }
    public bool Ativo { get; private set; }
    public ICollection<Agendamento> Agendamentos { get; private set; } = new List<Agendamento>();

    public void AtualizarNome(string nome)
    {
        Nome = nome.Trim();
    }

    public void AlterarPlano(TipoPlano tipoPlano)
    {
        TipoPlano = tipoPlano;
    }

    public void Ativar()
    {
        Ativo = true;
    }

    public void Inativar()
    {
        Ativo = false;
    }
}
