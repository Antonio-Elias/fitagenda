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

        builder.Property(x => x.CapacidadeMaxima)
            .IsRequired();

        builder.Property(x => x.DataHora)
            .IsRequired();

        builder.Property(x => x.Ativa)
            .IsRequired();

        builder.HasOne(x => x.TipoAula)
            .WithMany(t => t.Aulas)
            .HasForeignKey(x => x.CodigoTipoAula);
    }
}
