using CBP.Core.DomainObjects;
using CBP.ResponsavelPatrimonial.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBP.ResponsavelPatrimonial.API.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Responsavel>
    {
        public void Configure(EntityTypeBuilder<Responsavel> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            //builder.OwnsOne(c => c.Cpf, tf =>
            //{
            //    tf.Property(c => c.Numero)
            //        .IsRequired()
            //        .HasMaxLength(Cpf.CpfMaxLength)
            //        .HasColumnName("Cpf")
            //        .HasColumnType($"varchar({Cpf.CpfMaxLength})");
            //});

            builder.OwnsOne(c => c.Email, tf =>
            {
                tf.Property(c => c.Endereco)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasColumnType($"varchar({Email.EnderecoMaxLength})");
            });

            // 1 : 1
            builder.HasOne(c => c.Endereco)
                .WithOne(c => c.Responsavel);

            builder.ToTable("Responsavel");
        }
    }
}