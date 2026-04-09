using FitAgenda.Domain.Interfaces;
using FitAgenda.Domain.Models;
using FitAgenda.Domain.Validations;

namespace FitAgenda.Domain.Services;

public class AlunoService : BaseService, IAlunoService     
{
    public AlunoService(INotificador notificador)
        : base(notificador)
    {
    }

    public void Criar(Aluno aluno)
    {
        ExecutarValidacao(new AlunoValidation(), aluno);
    }

    public void Atualizar(Aluno aluno)
    {
        ExecutarValidacao(new AlunoValidation(), aluno);
    }
}
