namespace WayForPay.Domain;

/// <summary>
/// Your accout settinngs 
/// </summary>
public class MerchantSettings
{
    /// <summary>
    /// Seller identifier. This value is assigned to you from the side of WayForPay. 
    /// <remarks>Required</remarks>
    /// </summary>
    public string MerchantAccount { get; set; } = null!;

    /// <summary>
    /// Domain name of merchant’s website. 
    /// <remarks>Required</remarks>
    /// </summary>
    public string MerchantDomainName { get; set; } = null!;

    /// <summary>
    /// SecretKey. This value is assigned to you from the side of WayForPay. 
    /// <remarks>Required</remarks>
    /// </summary>
    public string SecretKey { get; set; } = null!;

    /// <summary>
    /// URL to which the system has to send a response with the payment result directly to the merchant. 
    /// <remarks>Required</remarks>
    /// </summary>
    public string ServiceUrl { get; set; } = null!;
}