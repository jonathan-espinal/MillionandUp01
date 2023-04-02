using Microsoft.EntityFrameworkCore;


namespace MillionandUpBackend01.Entities {
    public class DataContext : DbContext {

        // SessionService _session;

        public DataContext(DbContextOptions<DataContext> options) : base(options) {
        }

        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<ShoppingCart> ShoppingCart { get; set; } = null!;
        public virtual DbSet<ShoppingCartProduct> ShoppingCartProduct { get; set; } = null!;
        // public virtual DbSet<Storage> Storage { get; set; } = null!;

        /*
        protected virtual void AlterBaseEntity() {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));
            foreach (var entityEntry in entries) {
                if (_session.User.IsAuthenticated) {
                    ((BaseEntity)entityEntry.Entity).UpdatedAt = _session.User.DateTimeNow();
                    ((BaseEntity)entityEntry.Entity).UpdatedById = _session.User.Id;

                    if (entityEntry.State == EntityState.Added) {
                        ((BaseEntity)entityEntry.Entity).CreatedAt = _session.User.DateTimeNow();
                        ((BaseEntity)entityEntry.Entity).CreatedById = _session.User.Id;
                    }
                }
            }
        }

        public override int SaveChanges() {
            AlterBaseEntity();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
            AlterBaseEntity();
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>()
                .HasOne(p => p.CreatedBy)
                .WithMany()
                .HasForeignKey(p => p.CreatedById);

            modelBuilder.Entity<User>()
                .HasOne(p => p.UpdatedBy)
                .WithMany()
                .HasForeignKey(p => p.UpdatedById);

            modelBuilder.Entity<Storage>()
                .HasOne(p => p.CreatedBy)
                .WithMany()
                .HasForeignKey(p => p.CreatedById);

            modelBuilder.Entity<Storage>()
                .HasOne(p => p.UpdatedBy)
                .WithMany()
                .HasForeignKey(p => p.UpdatedById);
        }
        */
    }
}