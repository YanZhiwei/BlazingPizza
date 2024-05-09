using BlazingPizza.Components;
using BlazingPizza.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazingPizza;

public class Program
{
    public static void Main(string[] args)
    {
        //https://learn.microsoft.com/zh-cn/training/modules/interact-with-data-blazor-web-apps/7-exercise-share-data-in-blazor-applications
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        builder.Services.AddSingleton<PizzaService>();
        builder.Services.AddHttpClient();
        builder.Services.AddDbContext<PizzaStoreContext>(options =>
            options.UseSqlite("Data Source=pizza.db")
                .UseModel(BlazingPizza.PizzaStoreContextModel.Instance));

        builder.Services.AddDefaultIdentity<PizzaStoreUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<PizzaStoreContext>();

        builder.Services.AddIdentityServer()
            .AddApiAuthorization<PizzaStoreUser, PizzaStoreContext>();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();
        app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
        var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
        using (var scope = scopeFactory.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<PizzaStoreContext>();
            if (db.Database.EnsureCreated()) SeedData.Initialize(db);
        }

        app.Run();
    }
}