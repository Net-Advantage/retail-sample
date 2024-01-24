namespace RetailSample.Persistence;

public class RetailSampleDbContext(
    DbContextOptions<RetailSampleDbContext> options,
    IApplicationContext applicationContext)
    : TenantableDbContext<TenantEntity>(options, applicationContext)
{
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<StoreEntity> Stores => Set<StoreEntity>();
    public DbSet<ShoppingCartEntity> ShoppingCarts => Set<ShoppingCartEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Must call the base
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RetailSampleDbContext).Assembly);
    }
}
