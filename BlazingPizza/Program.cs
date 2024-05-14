using BlazingPizza.Components;
using BlazingPizza.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace BlazingPizza;

public class Program
{
    public static void Main(string[] args)
    {
        //https://learn.microsoft.com/zh-cn/training/modules/blazor-build-rich-interactive-components/5-exercise-improve-app-interactivity-lifecycle-events
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddRazorPages();
        builder.Services.AddRazorComponents().AddInteractiveServerComponents();
        builder.Services.AddDbContext<PizzaStoreContext>(options =>
            options.UseSqlite("Data Source=pizza.db"));
        builder.Services.AddHttpClient();
        builder.Services.AddScoped<OrderState>();

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
            if (db.Database.EnsureCreated())
                SeedData.Initialize(db);
        }

        app.Run();
    }
}