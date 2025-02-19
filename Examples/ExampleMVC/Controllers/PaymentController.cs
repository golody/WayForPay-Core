using ExampleMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WayForPay.Controllers;
using WayForPay.Domain;
using WayForPay.Services.Interfaces;

namespace ExampleMVC.Controllers;
// WayForPay Logic, be sure to use Services.AddWayForPay
[Route("/[action]")]
public class PaymentController : WayForPayController
{
    private readonly ExampleDbContext _dbContext;

    public PaymentController(ExampleDbContext dbContext, IWfpRequestsHandler wfpRequestsHandler, IWfpPurchaseService purchaseService) : base(wfpRequestsHandler, purchaseService)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> Pay(Order order)
    {
        var amount = order.Product.Price * order.Quantity;
        _dbContext.Users.Add(order.User);
        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();
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
                ClientFirstName = order.User.Name,
                ClientEmail = order.User.Email
            };
        
        return RedirectToPayment(rs, new RedirectionPageSettings() {
            Title = "Example Store Page - Redirecting to payment page...",
            VisibleContent = "<p>Change it if you want.</p>"
        });
    }
    
    // Do not specify routing, it maps automatically to MerchantSettings.ServiceUrl
    public override async Task<GateRequestResult> MerchantServiceEndpoint(GateRequestBody requestBody)
    {
        if (requestBody.TransactionStatus == "Approved") {
            // Handle here
            Order order = _dbContext.Orders.First(o => o.Id == Convert.ToInt32(requestBody.OrderReference));
            order.Paid = true;
            await _dbContext.SaveChangesAsync();
        }
        // Be sure to return it if all the operations was successful.
        // If not returned, wayforpay will try to send the request again for next 4 days
        // until the correct response is recieved.
        return GateAccept(requestBody);
    }
}