using FitAgenda.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitAgenda.Data.Mappings;

public class AgendamentoMapping : IEntityTypeConfiguration<Agendamento>
{
    public void Configure(EntityTypeBuilder<Agendamento> builder)
    {
        builder.ToTable("agendamentos");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Ativo)
            .IsRequired();

        builder.HasOne(x => x.Aluno)
            .WithMany(a => a.Agendamentos)
            .HasForeignKey(x => x.CodigoAluno);

        builder.HasOne(x => x.Aula)
            .WithMany(a => a.Agendamentos)
            .HasForeignKey(x => x.CodigoAula);

        builder.HasIndex(x => new { x.CodigoAluno, x.CodigoAula })
            .IsUnique();
    }
}
