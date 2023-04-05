using AutoMapper;
using bArtTestTask.Models;
using bArtTestTask.WebAPI.Models;
using bArtTestTask.WebAPI.Repositories.Implementations;
using bArtTestTask.WebAPI.Repositories.Interfaces;
using bArtTestTask.WebAPI.Services.Implementations;
using bArtTestTask.WebAPI.Services.Interfaces;

namespace bArtTestTask.WebAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IIncidentRepository, IncidentRepository>();
        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<IContactRepository, ContactRepository>();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IIncidentService, IncidentService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IContactService, ContactService>();
        return services;
    }

    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddSingleton<IMapper>(serviceProvider =>
        {
            var cfg = new MapperConfiguration(config =>
            {
                config.CreateMap<IncidentDto, Incident>();
                config.CreateMap<AccountDto, Account>();
                config.CreateMap<ContactDto, Contact>();
            });

            return cfg.CreateMapper();
        });
        return services;
    }
}