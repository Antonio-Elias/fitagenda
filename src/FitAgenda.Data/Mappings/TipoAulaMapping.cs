using FitAgenda.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitAgenda.Data.Mappings;

public class TipoAulaMapping : IEntityTypeConfiguration<TipoAula>
{
    public void Configure(EntityTypeBuilder<TipoAula> builder)
    {
        builder.ToTable("tipos_aula");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.Nome)
            .HasColumnName("nome")
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Ativo)
            .HasColumnName("ativo")
            .IsRequired();
    }
}
