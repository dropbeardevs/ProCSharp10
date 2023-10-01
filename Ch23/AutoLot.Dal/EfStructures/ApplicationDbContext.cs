namespace AutoLot.Dal.EfStructures;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CreditRisk> CreditRisks { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerOrderView> CustomerOrderViews { get; set; }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Make> Makes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<CarDriver> CarsToDrivers { get; set; }

    public virtual DbSet<Radio> Radios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CreditRisk>(entity =>
        {
            entity.Property(e => e.TimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.Customer).WithMany(p => p.CreditRisks).HasConstraintName("FK_CreditRisks_Customers");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.TimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        modelBuilder.Entity<CustomerOrderView>(entity => { entity.ToView("CustomerOrderView"); });

        modelBuilder.Entity<Make>(entity =>
        {
            entity.Property(e => e.TimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        new CarConfiguration().Configure(modelBuilder.Entity<Car>());

        new DriverConfiguration().Configure(modelBuilder.Entity<Driver>());

        new CarDriverConfiguration().Configure(modelBuilder.Entity<CarDriver>());

        new RadioConfiguration().Configure(modelBuilder.Entity<Radio>());

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.TimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.Car).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Inventory");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders).HasConstraintName("FK_Orders_Customers");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
