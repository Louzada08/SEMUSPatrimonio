using CBP.Core.DomainObjects;
using CBP.Usuarios.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBP.Usuarios.API.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
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

        }
    }
}