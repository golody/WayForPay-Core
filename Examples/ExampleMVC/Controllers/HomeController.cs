using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ExampleMVC.Models;

namespace ExampleMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ExampleDbContext _dbContext;

    public HomeController(ILogger<HomeController> logger, ExampleDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
        // Mock Data
        if (_dbContext.Products.Count() > 0) {
            return;
        }

        _dbContext.Products.AddRange([
            new Product {
                Name = "Product 1",
                Price = 0.05m
            },
            new Product {
                Name = "Product 2",
                Price = 0.1m
            },
            new Product {
                Name = "Product 3",
                Price = 0.15m
            }
        ]);
        _dbContext.SaveChanges();
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Products()
    {
        _logger.LogInformation(_dbContext.Products.Count().ToString());

        return View(_dbContext.Products);
    }

    public IActionResult BuyProduct(int id)
    {
        var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();
        var order = new Order() { Product = product };
        return View(order);
    }
}