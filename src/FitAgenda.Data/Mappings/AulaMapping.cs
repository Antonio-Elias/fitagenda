using FitAgenda.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitAgenda.Data.Mappings;

public class AulaMapping : IEntityTypeConfiguration<Aula>
{
    public void Configure(EntityTypeBuilder<Aula> builder)
    {
        builder.ToTable("aulas");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.CodigoTipoAula)
            .HasColumnName("codigo_tipo_aula")
            .IsRequired();

        builder.Property(x => x.CapacidadeMaxima)
            .HasColumnName("capacidade_maxima")
            .IsRequired();

        builder.Property(x => x.DataHora)
            .HasColumnName("data_hora")
            .IsRequired();

        builder.Property(x => x.Ativa)
            .HasColumnName("ativa")
            .IsRequired();

        builder.HasOne(x => x.TipoAula)
            .WithMany(t => t.Aulas)
            .HasForeignKey(x => x.CodigoTipoAula);
    }
}
