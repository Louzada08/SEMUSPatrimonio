using System.Linq;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using CBP.Transferencia.API.Model;

namespace CBP.Transferencia.API.Data
{
  public sealed class TermoTransferenciaContext : DbContext
  {
    public TermoTransferenciaContext(DbContextOptions<TermoTransferenciaContext> options)
        : base(options)
    {
      ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
      ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<TermoTransferenciaItem> TermoTransferenciaItens { get; set; }
    public DbSet<TermoTransferencia> TermoTransferencia { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
          e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
        property.SetColumnType("varchar(100)");

      modelBuilder.Ignore<ValidationResult>();

      foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

      modelBuilder.ApplyConfigurationsFromAssembly(typeof(TermoTransferenciaContext).Assembly);

    }
  }
}