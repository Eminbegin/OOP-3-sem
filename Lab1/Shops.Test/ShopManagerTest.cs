using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;
using Shops.Services;
using Xunit;

namespace Shops.Test;

public class ShopManagerTest
{
    private readonly ShopManager _shopManager = new ShopManager();

    [Theory]
    [InlineData("AbobaShop", "AbobaAdress")]
    [InlineData("DoraShop", "Nigeria")]
    public void CreateShop_ShopIsCreated(string name, string address)
    {
        Shop shop = _shopManager.RegistrateShop(name, address);
        Assert.Contains(shop, _shopManager.Shops);
    }

    [Fact]
    public void SuccessfulPurchase_BalanceDecreacedAndProductListIsEmptyAndShopBalanceIncreased()
    {
        Shop shop = _shopManager.RegistrateShop("239Shop", "Kirochnaya 8");
        Customer customer = _shopManager.RegistrateCustomer("Shilov Victor Ivanovich", 239239m);
        Product productFirst = _shopManager.SupplyProduct(shop, "Small balls", 300m, 2);
        Product productSecond = _shopManager.SupplyProduct(shop, "Big balls", 300000m, 2);
        _shopManager.AddToCart(customer, productFirst, 2);
        _shopManager.Purchase(shop, customer);
        Assert.Equal(239239m - (2 * 300m), customer.Balance);
        Assert.Empty(customer.ProductList);
        Assert.Equal(2 * 300m, shop.Balance);
    }

    [Fact]
    public void TryPurchase_ThrowExceptionNotEnoughMoney()
    {
        Shop shop = _shopManager.RegistrateShop("239Shop", "Kirochnaya 8");
        Customer customer = _shopManager.RegistrateCustomer("Shilov Victor Ivanovich", 239239m);
        Product productFirst = _shopManager.SupplyProduct(shop, "Small balls", 300m, 2);
        Product productSecond = _shopManager.SupplyProduct(shop, "Big balls", 300000m, 2);
        _shopManager.AddToCart(customer, productSecond, 2);
        Assert.Throws<InvalidPurchaseException>(() => _shopManager.Purchase(shop, customer));
    }

    [Fact]
    public void EfficientPurchase_ChoosedEfficientShopAndEmptyProductList()
    {
        Shop shop1 = _shopManager.RegistrateShop("239Shop", "Kirochnaya 8");
        Shop shop2 = _shopManager.RegistrateShop("Aboba", "AbobaAddress");
        Shop shop3 = _shopManager.RegistrateShop("DoraShop", "Nigeria");
        Shop shop4 = _shopManager.RegistrateShop("Diksi", "Netu addressa");
        Customer customer = _shopManager.RegistrateCustomer("Shilov Victor Ivanovich", 239239m);
        Product productFirst = _shopManager.RegistrateProduct("Small balls");
        Product productSecond = _shopManager.RegistrateProduct("Big balls");
        _shopManager.SupplyProduct(shop1, "Small balls", 100m, 1);
        _shopManager.SupplyProduct(shop2, "Small balls", 500m, 5);
        _shopManager.SupplyProduct(shop3, "Small balls", 300m, 2);
        _shopManager.SupplyProduct(shop4, "Small balls", 900m, 4);
        _shopManager.SupplyProduct(shop1, "Big balls", 800m, 4);
        _shopManager.SupplyProduct(shop2, "Big balls", 900m, 2);
        _shopManager.SupplyProduct(shop3, "Big balls", 200m, 1);
        _shopManager.SupplyProduct(shop4, "Big balls", 300m, 4);
        _shopManager.AddToCart(customer, productFirst, 2);
        _shopManager.AddToCart(customer, productSecond, 2);
        _shopManager.MinPurchase(customer);
        Assert.Equal(236839, customer.Balance);
        Assert.Empty(customer.ProductList);
        Assert.Equal(0m, shop1.Balance);
        Assert.Equal(0m, shop2.Balance);
        Assert.Equal(0m, shop3.Balance);
        Assert.Equal(2400m, shop4.Balance);
    }
}