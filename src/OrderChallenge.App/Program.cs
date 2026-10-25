using OrderChallenge.Core.Models;
using OrderChallenge.Core.Services;

var lines = new[] { new OrderLine("BOOK", 2, 30m), new OrderLine("PEN", 1, 10m) };
var calculator = new OrderCalculator();
var quote = calculator.Quote(lines, 10);
Console.WriteLine($"Subtotal: {quote.Subtotal:C}");
Console.WriteLine($"Discount: {quote.Discount:C}");
Console.WriteLine($"Shipping: {quote.Shipping:C}");
Console.WriteLine($"Total:    {quote.Total:C}");

var inventory = new InventoryService(new Dictionary<string, int> { ["BOOK"] = 3, ["PEN"] = 4 });
Console.WriteLine($"Reserved: {inventory.TryReserve(lines)}");
Console.WriteLine($"Books remaining: {inventory.Available("BOOK")}");
