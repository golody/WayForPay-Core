namespace WayForPay.Domain;


/// <summary>
/// Final request that has to be sent to WayForPay,
/// includes merchant signature, date and merchant settings
/// </summary>
public class PurchaseRequestBody : PurchaseRequestSettings {
    public string MerchantSignature { get; set; } = null!;
    public string MerchantAccount { get; set; } = null!;
    public string MerchantDomainName { get; set; } = null!;
    public string ServiceUrl { get; set; } = null!;
    public long OrderDate { get; set; }

    internal static PurchaseRequestBody FromSettings(PurchaseRequestSettings settings)
{
    return new PurchaseRequestBody
    {
        MerchantTransactionType = settings.MerchantTransactionType,
        Language = settings.Language,
        ReturnUrl = settings.ReturnUrl,
        OrderReference = settings.OrderReference,
        OrderNo = settings.OrderNo,
        Amount = settings.Amount,
        Currency = settings.Currency,
        AlternativeAmount = settings.AlternativeAmount,
        AlternativeCurrency = settings.AlternativeCurrency,
        HoldTimeout = settings.HoldTimeout,
        OrderLifetime = settings.OrderLifetime,
        OrderTimeout = settings.OrderTimeout,
        RecToken = settings.RecToken,
        ProductName = settings.ProductName,
        ProductPrice = settings.ProductPrice,
        ProductCount = settings.ProductCount,
        ClientAccountId = settings.ClientAccountId,
        SocialUri = settings.SocialUri,
        DeliveryList = settings.DeliveryList,
        ClientFirstName = settings.ClientFirstName,
        ClientLastName = settings.ClientLastName,
        ClientAddress = settings.ClientAddress,
        ClientCity = settings.ClientCity,
        ClientState = settings.ClientState,
        ClientZipCode = settings.ClientZipCode,
        ClientCountry = settings.ClientCountry,
        ClientEmail = settings.ClientEmail,
        ClientPhone = settings.ClientPhone,
        DeliveryFirstName = settings.DeliveryFirstName,
        DeliveryLastName = settings.DeliveryLastName,
        DeliveryAddress = settings.DeliveryAddress,
        DeliveryCity = settings.DeliveryCity,
        DeliveryState = settings.DeliveryState,
        DeliveryZipCode = settings.DeliveryZipCode,
        DeliveryCountry = settings.DeliveryCountry,
        DeliveryEmail = settings.DeliveryEmail,
        DeliveryPhone = settings.DeliveryPhone,
        RegularBehavior = settings.RegularBehavior,
        RegularMode = settings.RegularMode,
        RegularAmount = settings.RegularAmount,
        DateNext = settings.DateNext,
        RegularCountOrDateEnd = settings.RegularCountOrDateEnd,
        RegularOn = settings.RegularOn,
        PaymentSystems = settings.PaymentSystems,
        DefaultPaymentSystem = settings.DefaultPaymentSystem
    };
}
}