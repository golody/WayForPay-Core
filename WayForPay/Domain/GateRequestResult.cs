using Microsoft.AspNetCore.Mvc;

namespace WayForPay.Domain;
public class GateRequestResult : JsonResult
{
    internal GateRequestResult(object json) : base(json)
    {
        
    }
}