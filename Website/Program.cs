using MediatR.Pipeline;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

using Website.Infrastructure.Contexts;

using Website.Application;
using Website.Infrastructure.IRepositories;
using Website.Infrastructure.Repositories;
using NLog;
using Website.Infrastructure.LogServices;
using Website.Application.Middwares;
using Website.Extensions;

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

    app.UseMiddleware<ExceptionMiddleware>();

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

    builder.Services.AddDbContext<WebContext>(options =>
     options.UseNpgsql(strConn,
     o =>
     {
         o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
         o.CommandTimeout(250);
     }
     ));
    ConfigNLog(builder);
    builder.Services.ConfigDI();
    builder.Services.ConfigAutoMapper();
    builder.Services.AddApplication();

    // Add services to the container.
    builder.Services.AddControllersWithViews();
    return builder;
}

static void ConfigNLog(WebApplicationBuilder builder)
{
    var appBasePath = string.IsNullOrEmpty(builder.Configuration["LogsFolder"]) ? Directory.GetCurrentDirectory() : builder.Configuration["LogsFolder"];
    GlobalDiagnosticsContext.Set("appbasepath", appBasePath);
    LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config")).GetCurrentClassLogger();
}
#endregion