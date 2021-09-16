using Microsoft.EntityFrameworkCore;
using System.Linq;
using CBP.Core.Data;
using System.Threading.Tasks;
using CBP.Local.API.Models;
using FluentValidation.Results;
using CBP.Core.Messages;
using CBP.Core.DomainObjects;
using CBP.Core.Mediator;

namespace CBP.Local.API.Data
{
  public sealed class LocalContext : DbContext, IUnitOfWork
  {
    private readonly IMediatorHandler _mediatorHandler;

    public LocalContext(DbContextOptions<LocalContext> options, IMediatorHandler mediatorHandler)
        : base(options) 
    {
      _mediatorHandler = mediatorHandler;
      ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
      ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Unidade> Unidades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Ignore<ValidationResult>();
      modelBuilder.Ignore<Event>();

      foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
          e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
        property.SetColumnType("varchar(100)");

      foreach (var relationship in modelBuilder.Model.GetEntityTypes()
        .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

      modelBuilder.ApplyConfigurationsFromAssembly(typeof(LocalContext).Assembly);
    }

    public async Task<bool> Commit()
    {
      var sucesso = await base.SaveChangesAsync() > 0;
      if (sucesso) await _mediatorHandler.PublicarEventos(this);

      return sucesso;
    }
  }

  public static class MediatorExtension
  {
    public static async Task PublicarEventos<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
    {
      var domainEntities = ctx.ChangeTracker
          .Entries<Entity>()
          .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

      var domainEvents = domainEntities
          .SelectMany(x => x.Entity.Notificacoes)
          .ToList();

      domainEntities.ToList()
          .ForEach(entity => entity.Entity.LimparEventos());

      var tasks = domainEvents
          .Select(async (domainEvent) => {
            await mediator.PublicarEvento(domainEvent);
          });

      await Task.WhenAll(tasks);
    }
  }
}
