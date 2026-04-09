using FitAgenda.Domain.Models;

namespace FitAgenda.Domain.Interfaces;

public interface IAulaService
{
    void Criar(Aula aula, TipoAula tipoAula);
    void Atualizar(Aula aula, TipoAula tipoAula);
}
