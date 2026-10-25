namespace OrderChallenge.Core.Models;

public sealed record OrderQuote(decimal Subtotal, decimal Discount, decimal Shipping, decimal Total);
