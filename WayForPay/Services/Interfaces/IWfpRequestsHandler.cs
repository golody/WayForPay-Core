using Microsoft.Extensions.Options;
using WayForPay.Domain;

namespace WayForPay.Services.Interfaces;
public interface IWfpRequestsHandler {
    public PurchaseRequestBody GeneratePurchaseRequest(PurchaseRequestSettings ps);
    public MerchantSettings Settings { get; }
    public bool CheckGateRequestSignature(GateRequestBody requestBodyData);
    public string GeneratePurchaseSignature(PurchaseRequestBody data);
    
    public GateRequestBody DeserializeGateRequest(string json);
    
    public GateRequestResult GateAcceptResult(GateRequestBody requestBodyData);
    public string GenerateGateSignature(GateRequestBody wfpResponse, string timeOffset, string status);
}