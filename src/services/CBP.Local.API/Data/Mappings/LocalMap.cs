using CBP.Local.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBP.Local.API.Data.Mappings
{
  public class UnidadeMapping : IEntityTypeConfiguration<Unidade>
  {
    public void Configure(EntityTypeBuilder<Unidade> builder)
    {
      builder.HasKey(c => c.Id);

      builder.Property(c => c.Nome)
          .IsRequired()
          .HasColumnType("varchar(100)");

      // 1 : N => Unidade : Resonsaveis
      builder.HasMany(r => r.Responsavel)
                  .WithOne(u => u.Unidade)
                  .HasForeignKey(k => k.UnidadeId);

      builder.ToTable("Unidade");
    }
  }
}