using System.Reflection;
using FluentValidation;
using apibanca.application.Infrastructure.Data;
using apibanca.application.Behaviours;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace apibanca.application;

public static class ServiceExtensions
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }

    public static void AddInfrastuctureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApiContext>(c => c.UseInMemoryDatabase("apibanca"));
        //var context =  .ApplicationServices.GetService<ApiContext>();
        //AddTestData(context);        
    }
}