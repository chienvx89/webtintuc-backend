using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using Website.Infrastructure.Contexts;

WebApplicationBuilder builder = ConfigurationServices(args);

AppRun(builder);



#region local function
static void AppRun(WebApplicationBuilder builder)
{
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");

        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}

static WebApplicationBuilder ConfigurationServices(string[] args)
{

    var builder = WebApplication.CreateBuilder(args);
    var strConn = builder.Configuration.GetConnectionString("WebApiDatabase");

    //builder.Services.AddDbContext<WebContext>(options =>
    //  options.UseNpgsql(strConn, o =>
    //  {
    //      o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
    //      o.CommandTimeout(250);
    //  }));

    builder.Services.AddDbContext<WebContext>(options =>
        options.UseNpgsql(strConn,
        o =>
        {
            o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            o.CommandTimeout(250);
        }
        ));


    // Add services to the container.
    builder.Services.AddControllersWithViews();
    return builder;
}
#endregion