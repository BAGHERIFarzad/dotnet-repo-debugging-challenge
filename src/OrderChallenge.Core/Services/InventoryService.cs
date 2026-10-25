using OrderChallenge.Core.Models;

namespace OrderChallenge.Core.Services;

public sealed class InventoryService
{
    private readonly Dictionary<string, int> _stock;

    public InventoryService(IDictionary<string, int> initialStock)
    {
        ArgumentNullException.ThrowIfNull(initialStock);
        if (initialStock.Any(x => string.IsNullOrWhiteSpace(x.Key) || x.Value < 0))
            throw new ArgumentException("Invalid initial stock.", nameof(initialStock));
        _stock = new Dictionary<string, int>(initialStock, StringComparer.OrdinalIgnoreCase);
    }

    public int Available(string sku) => _stock.TryGetValue(sku, out var quantity) ? quantity : 0;

    // Reservation must be all-or-nothing. This implementation deliberately violates that contract.
    public bool TryReserve(IReadOnlyList<OrderLine> lines)
    {
        ArgumentNullException.ThrowIfNull(lines);
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line.Sku) || line.Quantity <= 0 || !_stock.TryGetValue(line.Sku, out var available) || available < line.Quantity)
                return false;
            _stock[line.Sku] -= line.Quantity;
        }
        return true;
    }
}
