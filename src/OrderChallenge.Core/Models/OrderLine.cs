namespace OrderChallenge.Core.Models;

public sealed record OrderLine(string Sku, int Quantity, decimal UnitPrice);
