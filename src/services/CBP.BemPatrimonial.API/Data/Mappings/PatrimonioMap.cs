using CBP.BemPatrimonial.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Cp.BemPatrimonial.API.Data.Mappings
{
  public class PatrimonioMap : IEntityTypeConfiguration<Patrimonio>
  {
    public void Configure(EntityTypeBuilder<Patrimonio> builder)
    {
      builder.ToTable("BemPatrimonial");
      builder.HasKey(p => p.Id);

      builder.Property(p => p.CodigoPatrimonio)
        .IsRequired(true);

      builder.Property(p => p.CodigoPatrimonioCP)
        .IsRequired(false);

      builder.Property(p => p.Descricao)
        .IsRequired(true)
        .HasColumnType<string>("character varying(250)");

      builder.Property(p => p.EstadoConservacaoId)
        .IsRequired(true);
      
      builder.Property(p => p.NumeroNotaFiscal)
        .IsRequired(false);

      builder.Property(p => p.LocalComplemento)
        .IsRequired(false)
        .HasColumnType<string>("character varying(30)");

      builder.Property(p => p.DataEntrada)
        .IsRequired(true)
        .HasColumnType<DateTime>("date");

      builder.Property(p => p.DataTransferencia)
        .IsRequired(false)
        .HasColumnType<DateTime?>("date");

      builder.Property(p => p.DataDoacao)
        .IsRequired(false)
        .HasColumnType<DateTime?>("date");

      builder.Property(p => p.DataEmprestimo)
        .IsRequired(false)
        .HasColumnType<DateTime?>("date");

      builder.Property(p => p.DataRetornoEmprestimo)
        .IsRequired(false)
        .HasColumnType<DateTime?>("date");

      builder.Property(p => p.DataBaixa)
        .IsRequired(false)
        .HasColumnType<DateTime?>("date");

      builder.Property(p => p.ValorBem)
        .IsRequired(true)
        .HasColumnType<decimal>("decimal(9,2)")
        .HasDefaultValue<decimal>(0);

      builder.Property(p => p.PessoaResponsavelId)
        .IsRequired(false);

      builder.HasOne(b => b.EstadoConservacoes)
        .WithMany(b => b.Patrimonios)
        .HasForeignKey(b => b.EstadoConservacaoId);

      builder.HasOne(b => b.PessoasResponsaveis)
        .WithMany(b => b.Patrimonios)
        .HasForeignKey(b => b.PessoaResponsavelId);

      builder.HasOne(b => b.Locais)
        .WithMany(b => b.Patrimonios)
        .HasForeignKey(b => b.LocalId);

      builder.HasIndex(p => p.CodigoPatrimonio)
        .HasName("idx_bempatrimonial_patrimonio")
        .IsUnique(true);

      builder.HasIndex(p => p.NumeroNotaFiscal)
        .HasName("idx_bempatrimonial_numeronotafiscal")
        .IsUnique(false);
    }
  }
}
