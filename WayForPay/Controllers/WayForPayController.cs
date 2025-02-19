using Microsoft.AspNetCore.Mvc;
using WayForPay.Domain;
using WayForPay.Filters;
using WayForPay.Services.Interfaces;

namespace WayForPay.Controllers;

/// <summary>
/// Base controller class for routing the incomming requests on <see cref="MerchantSettings.ServiceUrl"/> from WayForPay
/// to ServiceEndpointAccept.
/// Validation of the request signature is managed by <see cref="ServiceEndpointFilter"/>.
/// If validation fails, filter short-circuits MerchantServiceEndpoint of the action method with NonAuthorized result
/// <remarks> Requires <code>builder.Services.AddWayForPay()</code></remarks>
/// </summary>
public abstract class WayForPayController : Controller
{
    private readonly IWfpRequestsHandler _wfpRequestsHandler;
    private readonly IWfpPurchaseService _purchaseService;

    protected WayForPayController(IWfpRequestsHandler wfpRequestsHandler, IWfpPurchaseService purchaseService)
    {
        _wfpRequestsHandler = wfpRequestsHandler;
        _purchaseService = purchaseService;
    }

    /// <returns>Proper response on request on ServiceUrl</returns>
    protected GateRequestResult GateAccept(GateRequestBody requestBody)
    {
        return _wfpRequestsHandler.GateAcceptResult(requestBody);
    }

    protected IActionResult RedirectToPayment(PurchaseRequestSettings rs, RedirectionPageSettings? pageSettings = null)
    {
        var req = _wfpRequestsHandler.GeneratePurchaseRequest(rs);
        return _purchaseService.RedirectionPageResult(req, pageSettings);
    }

    /// <summary>
    /// Defines the merchant service endpoint logic to process payment result requests sent by WayForPay.
    /// <param name="requestBody">Body of the request, assigned in <see cref="ServiceEndpointFilter"/></param>
    /// The result of the request has to be sent using <see cref="ServiceEndpointAccept"/>,
    /// if not, WayForPay will try to resend the confirmation for next 4 days until it gets the proper response
    /// </summary>
    public abstract Task<GateRequestResult> MerchantServiceEndpoint(GateRequestBody requestBody);
}