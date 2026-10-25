using OrderChallenge.Core.Models;

namespace OrderChallenge.Core.Services;

public sealed class OrderCalculator
{
    // Business rules live in docs/BUSINESS_RULES.md. Some behavior here is intentionally incorrect.
    public OrderQuote Quote(IReadOnlyList<OrderLine> lines, int discountPercent)
    {
        ArgumentNullException.ThrowIfNull(lines);
        if (discountPercent < 0 || discountPercent > 100)
            throw new ArgumentOutOfRangeException(nameof(discountPercent));

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line.Sku) || line.Quantity <= 0 || line.UnitPrice < 0)
                throw new ArgumentException("Invalid order line.", nameof(lines));
        }

        // BUGS ARE INTENTIONAL. Investigate rather than changing tests to make them green.
        decimal subtotal = lines.Sum(x => x.UnitPrice);
        decimal discount = subtotal * (discountPercent / 100);
        decimal discountedSubtotal = subtotal - discount;
        decimal shipping = discountedSubtotal <= 50m ? 7.50m : 0m;
        decimal total = discountedSubtotal + shipping;

        return new OrderQuote(subtotal, discount, shipping, total);
    }
}
