namespace WayForPay.Domain;

public class GateRequestBody
{
    public string MerchantAccount { get; init; } = null!;
    public string OrderReference { get; init; } = null!;
    public string MerchantSignature { get; init; } = null!;
    public int Amount { get; init; }
    public string Currency { get; init; } = null!;
    public string AuthCode { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Phone { get; init; } = null!;
    public long CreatedDate { get; init; }
    public long ProcessingDate { get; init; }
    public string CardPan { get; init; } = null!;
    public string CardType { get; init; } = null!;
    public string IssuerBankCountry { get; init; } = null!;
    public string IssuerBankName { get; init; } = null!;
    public string RecToken { get; init; } = null!;
    public string TransactionStatus { get; init; } = null!;
    public string Reason { get; init; } = null!;
    public int ReasonCode { get; set; }
    public double Fee { get; set; }
    public string PaymentSystem { get; set; } = null!;
    public string AcquirerBankName { get; set; } = null!;
    public string CardProduct { get; set; } = null!;
    public string ClientName { get; set; } = null!;
}