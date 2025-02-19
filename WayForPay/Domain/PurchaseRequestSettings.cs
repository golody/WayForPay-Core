using System.ComponentModel.DataAnnotations;

namespace WayForPay.Domain;

public class PurchaseRequestSettings
{
    /// <summary>
    /// Transaction type. May take one of the following values: AUTO, AUTH, SALE. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? MerchantTransactionType { get; set; }

    /// <summary>
    /// Language of the payment page. May take one of the following values: AUTO (determined by browser language), RU (default), UA, EN, DE, IT, RO, ES, PL, SK, FR, LV, CS. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// URL to which the system transfers the client with the payment result. In case of absence, redirects to the payment result checkout PSP. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? ReturnUrl { get; set; }

    /// <summary>
    /// Unique number of the order in the merchant’s system. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? OrderReference { get; set; }

    /// <summary>
    /// Order number in the seller’s system. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? OrderNo { get; set; }

    /// <summary>
    /// Amount of the order. 
    /// <remarks>Required</remarks>
    /// </summary>
    [Required]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Currency of the order (e.g., UAH). 
    /// <remarks>Required</remarks>
    /// </summary>
    [Required]
    public string? Currency { get; set; }

    /// <summary>
    /// Alternative amount of the order. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public decimal? AlternativeAmount { get; set; }

    /// <summary>
    /// Alternative currency of the order (e.g., USD, EUR). 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? AlternativeCurrency { get; set; }

    /// <summary>
    /// Period of validity of funds blocking in seconds. Default: 1,728,000 (20 days). Max: 1,728,000. Min: 60. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public int? HoldTimeout { get; set; }

    /// <summary>
    /// Sets the interval during which an order can be paid (in seconds). 
    /// <remarks>Optional</remarks>
    /// </summary>
    public int? OrderLifetime { get; set; }

    /// <summary>
    /// Sets the interval within which the order can be paid for (in seconds). 
    /// <remarks>Optional</remarks>
    /// </summary>
    public int? OrderTimeout { get; set; }

    /// <summary>
    /// Card token for recurring withdrawals. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? RecToken { get; set; }

    /// <summary>
    /// Array with the names of the ordered products. 
    /// <remarks>Required</remarks>
    /// </summary>
    [Required]
    public string[]? ProductName { get; set; }

    /// <summary>
    /// Array with the prices per product unit. 
    /// <remarks>Required</remarks>
    /// </summary>
    [Required]
    public decimal[]? ProductPrice { get; set; }

    /// <summary>
    /// Array with the quantity of ordered products for each item. 
    /// <remarks>Required</remarks>
    /// </summary>
    [Required]
    public int[]? ProductCount { get; set; }

    /// <summary>
    /// Unique identifier in the merchant’s system (login, email, etc.). 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? ClientAccountId { get; set; }

    /// <summary>
    /// Unique identifier of a resource (e.g., social media link). 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? SocialUri { get; set; }

    /// <summary>
    /// The customer needs to fill out the delivery block. You can pass one or more values through ";". 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? DeliveryList { get; set; }

    /// <summary>
    /// First name of the client. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? ClientFirstName { get; set; }

    /// <summary>
    /// Client's surname. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? ClientLastName { get; set; }

    /// <summary>
    /// Client's address. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? ClientAddress { get; set; }

    /// <summary>
    /// Client's city. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? ClientCity { get; set; }

    /// <summary>
    /// Client's state/region. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? ClientState { get; set; }

    /// <summary>
    /// Client's postal code. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? ClientZipCode { get; set; }

    /// <summary>
    /// Client's country in digital ISO 3166-1-Alpha 3 format. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? ClientCountry { get; set; }

    /// <summary>
    /// Client's email. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? ClientEmail { get; set; }

    /// <summary>
    /// Client's phone number. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? ClientPhone { get; set; }

    /// <summary>
    /// Recipient's first name. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? DeliveryFirstName { get; set; }

    /// <summary>
    /// Recipient's surname. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? DeliveryLastName { get; set; }

    /// <summary>
    /// Recipient's address. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? DeliveryAddress { get; set; }

    /// <summary>
    /// Recipient's city. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? DeliveryCity { get; set; }

    /// <summary>
    /// Recipient's state/region. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? DeliveryState { get; set; }

    /// <summary>
    /// Recipient's postal code. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? DeliveryZipCode { get; set; }

    /// <summary>
    /// Recipient's country. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? DeliveryCountry { get; set; }

    /// <summary>
    /// Recipient's email. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? DeliveryEmail { get; set; }

    /// <summary>
    /// Recipient's phone number. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? DeliveryPhone { get; set; }

    /// <summary>
    /// Preset so that the client cannot edit the parameters of the regular payment on the payment page. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public bool? RegularBehavior { get; set; }

    /// <summary>
    /// Frequency of regular charges. May take one of the following values: client (displays a list of all available recurrence periods), none (payment is made without using a regular payment), once, daily, weekly, monthly, quarterly, halfyearly, yearly. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? RegularMode { get; set; }

    /// <summary>
    /// Amount of the regular payment. If not transferred, the amount is taken from the "Amount" field. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public decimal? RegularAmount { get; set; }

    /// <summary>
    /// Date of the first write-off for the regular payment. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public DateTime? DateNext { get; set; }

    /// <summary>
    /// End date or the number of payments. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? RegularCountOrDateEnd { get; set; }

    /// <summary>
    /// When passing value = 1, enables the checkbox "Make payment regular". 
    /// <remarks>Optional</remarks>
    /// </summary>
    public int? RegularOn { get; set; }

    /// <summary>
    /// The list of payment systems available for client selection on the payment page. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? PaymentSystems { get; set; }

    /// <summary>
    /// Payment system that will be initially displayed on the payment page for the payer. 
    /// <remarks>Optional</remarks>
    /// </summary>
    public string? DefaultPaymentSystem { get; set; }

    public PurchaseRequestSettings()
    {
        
    }
    public PurchaseRequestSettings(decimal amount, string currency, string[] productName, decimal[] productPrice, int[] productCount)
    {
        Amount = amount;
        Currency = currency;
        ProductName = productName ?? throw new ArgumentNullException(nameof(productName));
        ProductPrice = productPrice ?? throw new ArgumentNullException(nameof(productPrice));
        ProductCount = productCount ?? throw new ArgumentNullException(nameof(productCount));
    }
}