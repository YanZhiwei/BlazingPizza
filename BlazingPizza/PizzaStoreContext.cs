using BlazingPizza.Data;
using BlazingPizza.Model;
using BlazingPizza.Services;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;

namespace BlazingPizza;
[DbContext(typeof(PizzaStoreContext))]
public partial class PizzaStoreContextModel : RuntimeModel
{
    static PizzaStoreContextModel()
    {
        var model = new PizzaStoreContextModel();
        model.Initialize();
        model.Customize();
        _instance = model;
    }

    private static PizzaStoreContextModel _instance;
    public static IModel Instance => _instance;

    partial void Initialize();

    partial void Customize();
}
public class PizzaStoreContext : ApiAuthorizationDbContext<PizzaStoreUser>
{
    public PizzaStoreContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }

    public DbSet<Order> Orders { get; set; }

    public DbSet<Pizza> Pizzas { get; set; }

    public DbSet<PizzaSpecial> Specials { get; set; }

    public DbSet<Topping> Toppings { get; set; }

    public DbSet<NotificationSubscription> NotificationSubscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuring a many-to-many special -> topping relationship that is friendly for serialization
        modelBuilder.Entity<PizzaTopping>().HasKey(pst => new { pst.PizzaId, pst.ToppingId });
        modelBuilder.Entity<PizzaTopping>().HasOne<Pizza>().WithMany(ps => ps.Toppings);
        modelBuilder.Entity<PizzaTopping>().HasOne(pst => pst.Topping).WithMany();

        // Inline the Lat-Long pairs in Order rather than having a FK to another table
        modelBuilder.Entity<Order>().OwnsOne(o => o.DeliveryLocation);
    }
}

public class PizzaStoreUser : IdentityUser
{
}

public class NotificationSubscription
{
    public int? NotificationSubscriptionId { get; set; }

    public string? UserId { get; set; }

    public string? Url { get; set; }

    public string? P256dh { get; set; }

    public string? Auth { get; set; }
}