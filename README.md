# WayForPayCore
is a .NET Core library for integrating with the WayForPay payment system in ASP.NET Core MVC applications.
## Getting Started
Install NuGet package
```dotnet add package WayForPayCore --version 1.0.0```
### Configuration
Configure WayForPay settings using environment variables or in `appsettings.json`:
```json
{
    "WayForPay": {
        "MerchantAccount": "your_account_name",
        "MerchantDomainName": "your.domain",
        "SecretKey": "YourVerySecreyKey",
        "ServiceUrl": "/payment/gate"
    }
}
```
Register the library:<br>
`Program.cs`:
```c#
var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<MerchantSettings>(builder.Configuration.GetSection("WayForPay"));
builder.Services.AddWayForPay();
```
### Create a PaymentController to handle payments<br>
`Controllers/PaymentController.cs`:
```c#
public class PaymentController : WayForPayController
{
private readonly ExampleDbContext _dbContext;

    public PaymentController(ExampleDbContext dbContext, IWfpRequestsHandler wfpRequestsHandler, IWfpPurchaseService purchaseService) 
        : base(wfpRequestsHandler, purchaseService)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> Pay(Order order)
    {
        // Create Request settings
        PurchaseRequestSettings rs = 
            //Required Fields
            new(
                amount: amount, 
                currency: "EUR",
                productName: [order.Product.Name],
                productPrice: [order.Product.Price],
                productCount: [order.Quantity]
            ) 
            //Optional Fields
            {
                OrderReference = order.Id.ToString(),
                ClientFirstName = order.User.Name,
                ClientEmail = order.User.Email
            };
        
        return RedirectToPayment(rs, new RedirectionPageSettings() {
            Title = "Example Store - Redirecting to payment page...",
            VisibleContent = "<p>Change it if you want.</p>"
        });
    }

    public override async Task<GateRequestResult> MerchantServiceEndpoint(GateRequestBody requestBody)
    {
        // Handle the request
        if (requestBody.TransactionStatus == "Approved") 
        {
            var order = _dbContext.Orders.First(o => o.Id == Convert.ToInt32(requestBody.OrderReference));
            order.Paid = true;
            await _dbContext.SaveChangesAsync();
        }
        if (requestBody.TransactionStatus == "Refunded") 
        {
            var order = _dbContext.Orders.First(o => o.Id == Convert.ToInt32(requestBody.OrderReference));
            order.Paid = false;
            await _dbContext.SaveChangesAsync();
        }
        // If not returned, WayForPay will try to send this request until it gets the correct response 
        return GateAccept(requestBody);
    }
}
```
### Usage
Send a POST request to /Pay with order details to initiate payment.
WayForPay will handle the payment and notify your service via MerchantServiceEndpoint.
See the full usage in [Examples](https://github.com/golody/WayForPay-Core/tree/main/Examples/ExampleMVC)
