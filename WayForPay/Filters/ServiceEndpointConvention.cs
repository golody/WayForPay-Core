using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using WayForPay.Controllers;
using WayForPay.Domain;
using WayForPay.Services.Interfaces;

namespace WayForPay.Filters;

/// <summary>
/// Represents a convention used to configure endpoints for controllers that inherit from
/// <see cref="WayForPayController"/>. This convention applies routing using the <see cref="MerchantSettings.ServiceUrl"/>.
/// </summary>
public class ServiceEndpointConvention : IApplicationModelConvention
{
    private readonly IWfpRequestsHandler _wfpRequestsHandler;

    public ServiceEndpointConvention(IWfpRequestsHandler wfpRequestsHandler)
    {
        _wfpRequestsHandler = wfpRequestsHandler;
    }

    public void Apply(ApplicationModel application)
    {
        ControllerModel? controller = application.Controllers.FirstOrDefault(c =>
            typeof(WayForPayController).IsAssignableFrom(c.ControllerType));
        if (controller == null) {
            return;
        }

        ActionModel? action = controller.Actions.FirstOrDefault(a => 
            a.ActionName == nameof(WayForPayController.MerchantServiceEndpoint)
        );

        if (action == null) {
            throw new InvalidOperationException("MerchantServiceEndpoint Action not found");
        }

        action.Selectors.Clear();
        // MerchantSettings service url routing
        action.Selectors.Add(new SelectorModel {
            AttributeRouteModel = new AttributeRouteModel {
                Template = _wfpRequestsHandler.Settings.ServiceUrl
            },
            ActionConstraints = { new HttpMethodActionConstraint(new[] { "POST" }) } // Example: Treat as [HttpGet]
        });

        action.Filters.Add(new ServiceEndpointFilter(_wfpRequestsHandler));
    }
}