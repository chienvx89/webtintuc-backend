using MediatR.Pipeline;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using Website.Infrastructure.Contexts;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Website.Application;
using Website.Infrastructure.IRepositories;
using Website.Infrastructure.Repositories;

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
    // Cấu hình MediatR

   


    //builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
    //builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

    builder.Services.AddDbContext<WebContext>(options =>
        options.UseNpgsql(strConn,
        o =>
        {
            o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            o.CommandTimeout(250);
        }
        ));

    #region DI
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped(typeof(IUnitOfWorkBase<>), typeof(UnitOfWorkBase<>));
    builder.Services.AddScoped(typeof(IRepository<,,>), typeof(RepositoryBase<,,>));
    builder.Services.AddScoped(typeof(IRepository<,>), typeof(RepositoryBase<,>));
    builder.Services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
    #endregion

    //builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
    builder.Services.AddApplication();
    // Add services to the container.
    builder.Services.AddControllersWithViews();
    return builder;
}
#endregion