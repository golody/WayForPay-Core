namespace ExampleMVC.Models;

public class Order
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
    public User User { get; set; } = null!;
    public bool Paid { get; set; }
}