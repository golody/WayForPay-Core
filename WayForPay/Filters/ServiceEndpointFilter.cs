using Microsoft.AspNetCore.Mvc.Filters;
using WayForPay.Controllers;
using WayForPay.Domain;
using WayForPay.Services.Interfaces;

namespace WayForPay.Filters;

/// <summary>
/// A filter that intercepts and processes HTTP requests targeting Merchant Service Endpoint. 
/// Verifies request signatures for authentication and deserializes the request body for further use.
/// If the signature validation fails, the filter denies the request by returning an Unauthorized result.
/// </summary>
public class ServiceEndpointFilter : IActionFilter
{
    private readonly IWfpRequestsHandler _wfpRequestsHandler;
    public ServiceEndpointFilter(IWfpRequestsHandler wfpRequestsHandler)
    {
        _wfpRequestsHandler = wfpRequestsHandler;
    }
    
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.Controller is not WayForPayController) {
            throw new InvalidOperationException("ServiceEndpointFilter can only be used on MerchantServiceControllerBase");
        }
        
        if (context.ActionArguments["requestBody"] == null) {
            return;
        }

        HttpRequest request = context.HttpContext.Request;
        request.EnableBuffering();

        using StreamReader reader = new(request.Body);
        request.Body.Position = 0;
        string body = reader.ReadToEndAsync().Result;
        GateRequestBody requestBody = _wfpRequestsHandler.DeserializeGateRequest(body);
        
        // Check the request signature and set the request body
        if (_wfpRequestsHandler.CheckGateRequestSignature(requestBody)) {
            context.ActionArguments["requestBody"] = requestBody;
        } 
        else {
            context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}