using FitAgenda.Data.Context;
using FitAgenda.Domain.Interfaces;
using FitAgenda.Domain.Models;

namespace FitAgenda.Data.Repositories;

public class TipoAulaRepository : Repository<TipoAula>, ITipoAulaRepository
{
    public TipoAulaRepository(AppDbContext context) : base(context)
    {
    }
}
