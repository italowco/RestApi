using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestApi.Domain.Model;
using RestApi.Domain.Model.Interfaces;
using System.Security.Claims;
using Z.EntityFramework.Plus;

namespace RestApi.Infraestructure.Data
{
    public class DataContext : IdentityDbContext<IdentityUser>, IDataContext
    {
        private readonly IHttpContextAccessor _accessor;

        public DataContext(IHttpContextAccessor accessor, DbContextOptions<DataContext> options)
            : base(options)
        {
            _accessor = accessor;
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(IDataContext).Assembly);
            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(SaveChanges());
        }

        public override int SaveChanges()
        {
            var userEmail = _accessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);

            foreach (var entry in ChangeTracker.Entries<IEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = userEmail;
                        entry.Entity.CreatedAt = DateTimeOffset.Now;
                        break;
                    case EntityState.Modified:
                        entry.Property(nameof(IEntity.CreatedBy)).IsModified = false;
                        entry.Property(nameof(IEntity.CreatedAt)).IsModified = false;

                        entry.Entity.UpdatedBy = userEmail;
                        entry.Entity.UpdatedAt = DateTimeOffset.Now;
                        break;
                }
            }

            //var audit = new Audit
            //{
            //    CreatedBy = string.IsNullOrWhiteSpace(userEmail) ? "System" : userEmail
            //};

            //audit.PreSaveChanges(this);
            //audit.PostSaveChanges();

            //if (audit.Configuration.AutoSavePreAction != null)
            //{
            //    audit.Configuration.AutoSavePreAction(this, audit);
            //    base.SaveChanges();
            //}

            return base.SaveChanges();

        }


        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }



    }
}