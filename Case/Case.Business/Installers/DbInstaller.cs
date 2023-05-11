using System.Reflection;
using Case.Business.Integrations.UnitedPayment.Services;
using Case.Entity.Entities;
using Case.Entity.GenericRepository;
using Case.Entity.GenericRepository.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Case.Business.Installers;

public static class DbInstaller
{
    public static void AddDbContextService(this IServiceCollection services)
    {
        services.AddDbContext<CaseDbContext>();
    }
    public static void ConfigureUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUnitedPaymentService, UnitedPaymentService>();
        
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(assembly);
    }
    
}