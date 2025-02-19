using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using WayForPay.Domain;
using WayForPay.Services.Interfaces;

namespace WayForPay.Services;

/// <summary>
/// Service for handling purchase integration requests with the WayForPay payment gateway.
/// </summary>
public class WfpPurchaseService : IWfpPurchaseService
{
    private const string BaseUrl = "https://secure.wayforpay.com/pay";
    private readonly IWfpRequestsHandler _requestsHandler;

    public WfpPurchaseService(IWfpRequestsHandler requestsHandler)
    {
        _requestsHandler = requestsHandler;
    }
    
    private List<string[]> RequestToDictionary(PurchaseRequestBody ps)
    {
        // have to use this instead of Dictionary<> bcs. 
        // wayforpay api expects the arrays to be written as
        // {"array[]": "1"},
        // {"array[]": "2"}...
        List<string[]> dictionary = new();

        var properties = ps.GetType().GetProperties();

        foreach (PropertyInfo property in properties) {
            if (property.PropertyType.IsArray) {
                if (property.GetValue(ps) is Array array) {
                    foreach (object item in array) {
                        string arrayKey = 
                            $"{char.ToLowerInvariant(property.Name[0]) + property.Name[1..]}[]";
                        dictionary.Add([arrayKey, item?.ToString() ?? string.Empty]);
                    }
                }
                continue;
            }
            string? value = property.GetValue(ps)?.ToString();
            if (value == null) {
                continue;
            }
            string key = char.ToLowerInvariant(property.Name[0]) + property.Name[1..];
            dictionary.Add([key, value]);
        }

        return dictionary;
    }

    public IActionResult RedirectionPageResult(PurchaseRequestBody requestSettings, RedirectionPageSettings? pageSettings = null)
    {
        pageSettings ??= new RedirectionPageSettings();
        PurchaseRequestBody purchaseRequest = _requestsHandler.GeneratePurchaseRequest(requestSettings);
        var reqDictionary = RequestToDictionary(purchaseRequest);
        string page = GenerateRedirectionPage(reqDictionary, pageSettings);
        return new ContentResult {
            Content = page,
            ContentType = "text/html"
        };
    }
    private string GenerateRedirectionPage(List<string[]> requestDict, RedirectionPageSettings pageSettings) {
        var sb = new StringBuilder();
        
        sb.AppendLine("<!DOCTYPE html>");
        sb.AppendLine("<html>");
        sb.AppendLine($"<head><title>{pageSettings.VisibleContent}</title></head>");
        sb.AppendLine("<body onload=\"document.forms[0].submit();\">");
        sb.AppendLine($"<form action=\"{BaseUrl}\" method=\"post\">");

        foreach (string[] request in requestDict) {
            sb.AppendLine($"<input type=\"hidden\" name=\"{request[0]}\" value=\"{request[1]}\" />");
        }
        
        sb.AppendLine($"<p>{pageSettings.VisibleContent}</p>");
        sb.AppendLine($"<input type=\"submit\" value=\"Continue\">");
        sb.AppendLine("</form>");
        sb.AppendLine("</body>");
        sb.AppendLine("</html>");

        return sb.ToString();
    }
}