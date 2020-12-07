using Microsoft.EntityFrameworkCore;
using CBP.BemPatrimonial.API.Models;
using System.Linq;
using CBP.Core.Data;
using System.Threading.Tasks;
using CBP.Core.Messages;
using FluentValidation.Results;

namespace CBP.BemPatrimonial.API.Data 
{
  public class PatrimonioContext : DbContext, IUnitOfWork
  {
    public PatrimonioContext(DbContextOptions<PatrimonioContext> options)
        : base(options) { }

    public DbSet<Patrimonio> Patrimonios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Ignore<ValidationResult>();
      modelBuilder.Ignore<Event>();

      foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
          e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
        property.SetColumnType("varchar(100)");

      modelBuilder.ApplyConfigurationsFromAssembly(typeof(PatrimonioContext).Assembly);
    }

    public async Task<bool> Commit()
    {
      return await base.SaveChangesAsync() > 0;
    }
  }
}
