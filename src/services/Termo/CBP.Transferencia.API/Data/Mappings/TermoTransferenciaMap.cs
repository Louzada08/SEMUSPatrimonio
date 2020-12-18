using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CBP.Transferencia.API.Model;

namespace CBP.Transferencia.API.Data.Mappings
{
  public class TermoTransferenciaMap : IEntityTypeConfiguration<TermoTransferencia>
  {
    public void Configure(EntityTypeBuilder<TermoTransferencia> builder)
    {
      builder.HasKey(c => c.Id);

      builder.Property(c => c.Motivo)
          .IsRequired(false)
          .HasColumnType("varchar(250)");

      builder.HasIndex(c => c.ResponsavelCedenteId)
          .HasName("IDX_Cliente");

      builder.HasMany(c => c.Itens)
          .WithOne(i => i.TermoTransferencia)
          .HasForeignKey(c => c.TermoTransferenciaId);

      builder.ToTable("TermoTransferencia");
    }
  }
}