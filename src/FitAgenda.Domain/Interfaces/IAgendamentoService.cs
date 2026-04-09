using FitAgenda.Domain.Models;

namespace FitAgenda.Domain.Interfaces;

public interface IAgendamentoService
{
    Agendamento? Agendar(Aluno aluno, Aula aula);
    void Cancelar(Agendamento agendamento);
}
