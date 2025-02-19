using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using WayForPay.Domain;
using WayForPay.Services.Interfaces;
using JsonException = System.Text.Json.JsonException;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WayForPay.Services;

/// <summary>
/// Service for generating signatures and 
/// </summary>
internal class WfpRequestsHandler : IWfpRequestsHandler
{
    private readonly JsonSerializerOptions _serializerOptions = new() {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
    public MerchantSettings Settings { get; }

    public WfpRequestsHandler(IOptions<MerchantSettings> settings)
    {
        if (settings.Value.SecretKey == null) {
            
        }
        Settings = settings.Value;
    }
    
    public GateRequestBody DeserializeGateRequest(string json)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// This method creates the answer on ServiceUrl request
    /// with merchant settings and signature
    /// </summary>
    /// <param name="requestBodyData">Request Data</param>
    /// <returns></returns>
    public GateRequestResult GateAcceptResult(GateRequestBody requestBodyData)
    {
        long time = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        const string status = "accept";
        string signature = GenerateGateSignature(requestBodyData, time.ToString(), status);
        return new GateRequestResult(new {
            orderReference = requestBodyData.OrderReference,
            status,
            time,
            signature
        });
    }

    /// <summary>
    /// Deserialize the request that was sent from WayForPay on service url
    /// with payment result
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    /// <exception cref="JsonException"></exception>
    public GateRequestBody DeserializeServiceRequest(string json)
    {
        GateRequestBody response = JsonSerializer.Deserialize<GateRequestBody>(
            json,
            _serializerOptions
        ) ?? throw new JsonException("Invalid response");

        return response;
    }

    public bool CheckGateRequestSignature(GateRequestBody requestBodyData)
    {
        if (String.IsNullOrEmpty(requestBodyData.MerchantSignature)) {
            return false;
        }
        
        StringBuilder signatureBuilder = new();

        signatureBuilder.Append(requestBodyData.MerchantAccount).Append(';');
        signatureBuilder.Append(requestBodyData.OrderReference).Append(';');
        signatureBuilder.Append(requestBodyData.Amount).Append(';');
        signatureBuilder.Append(requestBodyData.Currency).Append(';');
        signatureBuilder.Append(requestBodyData.AuthCode).Append(';');
        signatureBuilder.Append(requestBodyData.CardPan).Append(';');
        signatureBuilder.Append(requestBodyData.TransactionStatus).Append(';');
        signatureBuilder.Append(requestBodyData.ReasonCode);

        var hash = ComputeHmacMd5(signatureBuilder.ToString());
        return hash == requestBodyData.MerchantSignature;
    }

    
    
    /// <summary>
    /// This method creates the Final Purchase Request
    /// with merchant settings and signature
    /// </summary>
    /// <param name="ps">Request data</param>
    /// <returns>Serealized object</returns>
    /// <exception cref="ValidationException">
    /// If PaymentRequestSettings required fields are null or empty
    /// </exception>
    public PurchaseRequestBody GeneratePurchaseRequest(PurchaseRequestSettings ps)
    {
        List<ValidationResult> validationResults = ValidatePurchaseRequest(ps);
        if (validationResults.Count > 0) {
            throw new ValidationException(string.Join('\n', validationResults));
        }

        long orderDate = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        PurchaseRequestBody requestBody = PurchaseRequestBody.FromSettings(ps);

        requestBody.OrderDate = orderDate;
        requestBody.MerchantAccount = Settings.MerchantAccount;
        requestBody.MerchantDomainName = Settings.MerchantDomainName;
        requestBody.ServiceUrl = Settings.ServiceUrl;
        requestBody.MerchantSignature = GeneratePurchaseSignature(requestBody);

        return requestBody;
    }
    
    
    private List<ValidationResult> ValidatePurchaseRequest(PurchaseRequestSettings ps)
    {
        var context = new ValidationContext(ps, null, null);
        var results = new List<ValidationResult>();

        Validator.TryValidateObject(ps, context, results, true);

        return results;
    }


    public string GeneratePurchaseSignature(PurchaseRequestBody data)
    {
        StringBuilder signatureBuilder = new StringBuilder();
        signatureBuilder.Append(data.MerchantAccount).Append(";");
        signatureBuilder.Append(data.MerchantDomainName).Append(";");
        signatureBuilder.Append(data.OrderReference).Append(";");
        signatureBuilder.Append(data.OrderDate).Append(";");
        signatureBuilder.Append(data.Amount).Append(";");
        signatureBuilder.Append(data.Currency).Append(";");

        signatureBuilder.Append(string.Join(";", data.ProductName!)).Append(";");
        signatureBuilder.Append(string.Join(";", data.ProductCount!)).Append(";");
        signatureBuilder.Append(string.Join(";", data.ProductPrice!));

        var hash = ComputeHmacMd5(signatureBuilder.ToString());
        return hash;
    }

    public string GenerateGateSignature(GateRequestBody wfpResponse, string timeOffset, string status)
    {
        StringBuilder signatureBuilder = new StringBuilder();
        signatureBuilder.Append(wfpResponse.OrderReference).Append(';');
        signatureBuilder.Append(status).Append(';');
        signatureBuilder.Append(timeOffset);

        var hash = ComputeHmacMd5(signatureBuilder.ToString());
        return hash;
    }

    private string ComputeHmacMd5(string message)
    {
        using HMACMD5 hmac = new HMACMD5(
            Encoding.UTF8.GetBytes(Settings.SecretKey)
        );

        var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));

        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }
}