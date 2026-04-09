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

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.CodigoAluno)
            .HasColumnName("codigo_aluno")
            .IsRequired();

        builder.Property(x => x.CodigoAula)
            .HasColumnName("codigo_aula")
            .IsRequired();

        builder.Property(x => x.DataAgendamento)
            .HasColumnName("data_agendamento")
            .IsRequired();

        builder.Property(x => x.Ativo)
            .HasColumnName("ativo")
            .IsRequired();

        builder.HasOne(x => x.Aluno)
            .WithMany(a => a.Agendamentos)
            .HasForeignKey(x => x.CodigoAluno);

        builder.HasOne(x => x.Aula)
            .WithMany(a => a.Agendamentos)
            .HasForeignKey(x => x.CodigoAula);

        builder.HasIndex(x => new { x.CodigoAluno, x.CodigoAula })
            .HasDatabaseName("ix_agendamentos_codigo_aluno_codigo_aula")
            .IsUnique();
    }
}
