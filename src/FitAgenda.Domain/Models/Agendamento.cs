namespace FitAgenda.Domain.Models;

public class Agendamento : Entity
{
    protected Agendamento()
    {
    }

    public Agendamento(Guid codigoAluno, Guid codigoAula)
    {
        CodigoAluno = codigoAluno;
        CodigoAula = codigoAula;
        DataAgendamento = DateTime.UtcNow;
        Ativar();
    }

    public Guid CodigoAluno { get; private set; }
    public Aluno Aluno { get; private set; } = null!;

    public Guid CodigoAula { get; private set; }
    public Aula Aula { get; private set; } = null!;

    public DateTime DataAgendamento { get; private set; }
    public bool Ativo { get; private set; }

    public void Ativar()
    {
        Ativo = true;
    }

    public void Cancelar()
    {
        Ativo = false;
    }
}
