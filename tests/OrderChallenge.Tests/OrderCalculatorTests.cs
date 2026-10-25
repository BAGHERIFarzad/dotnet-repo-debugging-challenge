using OrderChallenge.Core.Models;
using OrderChallenge.Core.Services;

namespace OrderChallenge.Tests;

public class OrderCalculatorTests
{
    private readonly OrderCalculator _sut = new();

    [Fact]
    public void Subtotal_Multiplies_UnitPrice_By_Quantity()
    {
        var result = _sut.Quote([new("BOOK", 3, 20m)], 0);
        Assert.Equal(60m, result.Subtotal);
    }

    [Fact]
    public void Discount_Applies_Percentage_With_Decimal_Precision()
    {
        var result = _sut.Quote([new("BOOK", 1, 100m)], 15);
        Assert.Equal(15m, result.Discount);
        Assert.Equal(85m, result.Total);
    }

    [Fact]
    public void Shipping_Is_Free_At_Exactly_Fifty()
    {
        var result = _sut.Quote([new("BOOK", 1, 50m)], 0);
        Assert.Equal(0m, result.Shipping);
        Assert.Equal(50m, result.Total);
    }

    [Fact]
    public void Shipping_Is_Charged_Below_Fifty()
    {
        var result = _sut.Quote([new("BOOK", 1, 40m)], 0);
        Assert.Equal(7.50m, result.Shipping);
        Assert.Equal(47.50m, result.Total);
    }

    [Fact]
    public void Invalid_Discount_Is_Rejected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Quote([new("BOOK", 1, 10m)], 101));
    }

    [Fact]
    public void Discount_Uses_PreShipping_Subtotal()
    {
        var result = _sut.Quote([new("BOOK", 1, 60m)], 10);
        Assert.Equal(6m, result.Discount);
        Assert.Equal(54m, result.Total);
    }
}
