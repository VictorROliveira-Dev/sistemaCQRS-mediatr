using CleanArch.Domain.Abstractions;
using CleanArch.Infrastructure.Context;
using CleanArch.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace CleanArch.CrossCutting.AppDependencies;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var sqlConnection = configuration.GetConnectionString("DefaultCS");

        services.AddEntityFrameworkSqlServer().AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("DefaultCS"));
        });

        services.AddSingleton<IDbConnection>(provider =>
        {
            var connection = new SqlConnection(sqlConnection);
            connection.Open();
            return connection;
        });

        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IMemberDapperRepository, MemberDapperRepository>();

        var myHandlers = AppDomain.CurrentDomain.Load("CleanArch.Application");
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(myHandlers));

        return services;
    }
}
