using Business.AbstractFactories;
using Business.Services;
using Business.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<ISessionService, SessionService>();
        collection.AddScoped<IMessageService, MessageService>();
        collection.AddScoped<ICreatingService, CreatingService>();
        collection.AddScoped<ISendingMethodsService, SendingMethodService>();
        collection.AddScoped<IReportService, ReportService>();
        return collection;
    }
}