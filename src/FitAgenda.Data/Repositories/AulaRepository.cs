using FitAgenda.Data.Context;
using FitAgenda.Domain.Interfaces;
using FitAgenda.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FitAgenda.Data.Repositories;

public class AulaRepository : Repository<Aula>, IAulaRepository
{
    public AulaRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Aula?> ObterAulaComTipo(Guid codigoAula)
    {
        return await Db.Aulas
            .Include(aula => aula.TipoAula)
            .FirstOrDefaultAsync(aula => aula.Id == codigoAula);
    }

    public async Task<List<Aula>> ObterAulasComTipo()
    {
        return await Db.Aulas
            .Include(aula => aula.TipoAula)
            .ToListAsync();
    }
}
