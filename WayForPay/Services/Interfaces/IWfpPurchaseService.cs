using Microsoft.AspNetCore.Mvc;
using WayForPay.Domain;

namespace WayForPay.Services.Interfaces;

public interface IWfpPurchaseService
{
    public IActionResult RedirectionPageResult(PurchaseRequestBody requestSettings, RedirectionPageSettings? pageSettings = null);
}