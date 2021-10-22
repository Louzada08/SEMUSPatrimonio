using CBP.ResponsavelPatrimonial.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBP.ResponsavelPatrimonial.API.Data.Mappings
{
    public class UnidadeMapping : IEntityTypeConfiguration<Unidade>
    {
        public void Configure(EntityTypeBuilder<Unidade> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            // 1 : n => Unidade : Responsalvel
            builder.HasMany(r => r.Responsavel)
                .WithOne(u => u.Unidade)
                .HasForeignKey(fk => fk.UnidadeId);

            builder.ToTable("Unidade");
        }
    }
}