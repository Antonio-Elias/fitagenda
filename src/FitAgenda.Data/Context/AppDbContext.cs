using FitAgenda.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FitAgenda.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Aula> Aulas { get; set; }
    public DbSet<TipoAula> TiposAula { get; set; }
    public DbSet<Agendamento> Agendamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

}
