using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WayForPay.Controllers;
using WayForPay.Domain;
using WayForPay.Filters;
using WayForPay.Services;
using WayForPay.Services.Interfaces;

namespace WayForPay.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddWayForPay(
        this IServiceCollection services)
    {
        var merchantSettings = services.BuildServiceProvider().GetRequiredService<IOptions<MerchantSettings>>().Value;
        if (merchantSettings?.MerchantAccount == null || merchantSettings?.SecretKey == null) {
            throw new ApplicationException("MerchantSettings not found. Add it to the services using builder.Services.Configure<MerchantSettings>(...).");
        }
        services.AddScoped<IWfpRequestsHandler, WfpRequestsHandler>();
        services.AddTransient<IWfpPurchaseService, WfpPurchaseService>();
        services.AddControllers(options => {
            options.Conventions.Add(new ServiceEndpointConvention(services.BuildServiceProvider().GetRequiredService<IWfpRequestsHandler>()));
        });
        return services;
    }
}