using BlazingPizza.Components;
using BlazingPizza.Data;

namespace BlazingPizza;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        builder.Services.AddSingleton<PizzaService>();
        builder.Services.AddHttpClient();
        builder.Services.AddSqlite<PizzaStoreContext>("Data Source=pizza.db");
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