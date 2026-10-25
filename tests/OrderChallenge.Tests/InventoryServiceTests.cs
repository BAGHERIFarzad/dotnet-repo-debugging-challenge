using OrderChallenge.Core.Models;
using OrderChallenge.Core.Services;

namespace OrderChallenge.Tests;

public class InventoryServiceTests
{
    [Fact]
    public void Successful_Reservation_Decrements_Stock()
    {
        var sut = new InventoryService(new Dictionary<string, int> { ["A"] = 5 });
        Assert.True(sut.TryReserve([new OrderLine("A", 2, 10m)]));
        Assert.Equal(3, sut.Available("A"));
    }

    [Fact]
    public void Failed_Reservation_Does_Not_Partially_Mutate_Stock()
    {
        var sut = new InventoryService(new Dictionary<string, int> { ["A"] = 5, ["B"] = 1 });
        Assert.False(sut.TryReserve([new("A", 2, 1m), new("B", 2, 1m)]));
        Assert.Equal(5, sut.Available("A"));
        Assert.Equal(1, sut.Available("B"));
    }

    [Fact]
    public void Duplicate_SKUs_Are_Evaluated_Together()
    {
        var sut = new InventoryService(new Dictionary<string, int> { ["A"] = 5 });
        Assert.False(sut.TryReserve([new("A", 3, 1m), new("a", 3, 1m)]));
        Assert.Equal(5, sut.Available("A"));
    }

    [Fact]
    public void Unknown_SKU_Is_Rejected_Without_Mutation()
    {
        var sut = new InventoryService(new Dictionary<string, int> { ["A"] = 5 });
        Assert.False(sut.TryReserve([new("UNKNOWN", 1, 1m)]));
        Assert.Equal(5, sut.Available("A"));
    }
}
