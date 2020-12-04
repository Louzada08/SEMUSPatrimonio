using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CBP.ResponsavelPatrimonial.API.Models;

namespace CBP.ResponsavelPatrimonial.API.Data.Mappings
{
    public class EnderecoMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Logradouro)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.Property(c => c.Numero)
                .IsRequired()
                .HasColumnType("char(10)");

            builder.Property(c => c.Cep)
                .IsRequired()
                .HasColumnType("char(10)");

            builder.Property(c => c.Complemento)
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Bairro)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Cidade)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Estado)
                .IsRequired()
                .HasColumnType("char(02)");

            builder.ToTable("Endereco");
        }
    }
}