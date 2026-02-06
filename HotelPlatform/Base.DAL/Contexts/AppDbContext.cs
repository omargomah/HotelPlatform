using Base.DAL.Models.BaseModels;
using Base.Shared.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Base.DAL.Contexts
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
         
            #region make the default behavier of Delet is Restrict and make filter on all tables to don't return the object that is deleted
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var foreignKey in entityType.GetForeignKeys())
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }
                var isDeletedProperty = entityType.FindProperty("IsDeleted");
                if (isDeletedProperty != null && isDeletedProperty.ClrType == typeof(bool))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var filterExpression = Expression.Lambda(
                        Expression.Equal(
                            Expression.Property(parameter, "IsDeleted"),
                            Expression.Constant(false)
                        ),
                        parameter
                    );

                    entityType.SetQueryFilter(filterExpression);
                }
            }
            #endregion


            #region rename the tables of Identity
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("Users", "Security");
                entity.Property(e => e.FullName).HasComputedColumnSql("[FName]+' '+[LName]");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable("Roles", "Security");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles", "Security");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims", "Security");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins", "Security");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims", "Security");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens", "Security");
            });
            #endregion
            
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            string? _userId = null;
            if (_httpContextAccessor.HttpContext is not null)
            {
                var reqservices = _httpContextAccessor.HttpContext.RequestServices;
                if (reqservices is not null)
                    using (var scope = reqservices.CreateScope())
                    {
                        var services = scope.ServiceProvider;
                        if (services is not null)
                            try
                            {
                                var _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                                if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                                {
                                    var user = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;
                                    _userId = user?.Id;
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                    }
            }

            #region work as trigger that make update to UpdatedAt and CreatedAt and add the user id that make the Update or ADD
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UndatedAt = DateTime.Now;
                ((BaseEntity)entityEntry.Entity).UpdatedById = _userId;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
                    ((BaseEntity)entityEntry.Entity).CreatedById = _userId;
                }
            }
            #endregion
           
            return base.SaveChangesAsync(cancellationToken);
        }

        #region DBSets
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<OtpEntry> OtpEntries { get; set; }
        public DbSet<BlacklistedToken> BlacklistedTokens { get; set; }
        #endregion

    }
}
