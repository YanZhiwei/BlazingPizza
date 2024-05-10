using BlazingPizza.Components;
using BlazingPizza.Repository;
using BlazingPizza.Repository.Entities;
using BlazingPizza.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazingPizza;

public class Program
{
    public static void Main(string[] args)
    {
        //https://learn.microsoft.com/zh-cn/training/modules/use-pages-routing-layouts-control-blazor-navigation/
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddRazorPages();
        builder.Services.AddRazorComponents().AddInteractiveServerComponents();
        builder.Services.AddDbContext<PizzaStoreContext>(options =>
            options.UseSqlite("Data Source=pizza.db")
                .UseModel(PizzaStoreContextModel.Instance));

        builder.Services.AddDefaultIdentity<PizzaStoreUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<PizzaStoreContext>();

        builder.Services.AddIdentityServer()
            .AddApiAuthorization<PizzaStoreUser, PizzaStoreContext>();

        builder.Services.AddAuthentication()
            .AddIdentityServerJwt();
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
        app.UseAuthentication();
        app.UseIdentityServer();
        app.UseAuthorization();
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