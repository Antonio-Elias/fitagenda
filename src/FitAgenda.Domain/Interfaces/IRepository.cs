using FitAgenda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FitAgenda.Domain.Interfaces;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
    Task<TEntity?> ObterPorId(Guid id);
    Task<List<TEntity>> ObterTodos();
    Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
    Task Adicionar(TEntity entity);
    Task Atualizar(TEntity entity);
    Task<int> SaveChanges();
}
