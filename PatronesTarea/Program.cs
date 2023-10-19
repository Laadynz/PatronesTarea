using System;
using System.Collections.Generic;


interface IProduct
{
    string GetName();
    decimal GetPrice();
}


class Product : IProduct
{
    private string _name;
    private decimal _price;

    public Product(string name, decimal price)
    {
        _name = name;
        _price = price;
    }

    public string GetName()
    {
        return _name;
    }

    public decimal GetPrice()
    {
        return _price;
    }
}


class ProductAdapter : IProduct
{
    private Product _product;

    public ProductAdapter(Product product)
    {
        _product = product;
    }

    public string GetName()
    {
        return _product.GetName();
    }

    public decimal GetPrice()
    {
        return _product.GetPrice();
    }
}


abstract class ProductDecorator : IProduct
{
    protected IProduct _product;

    public ProductDecorator(IProduct product)
    {
        _product = product;
    }

    public virtual string GetName()
    {
        return _product.GetName();
    }

    public virtual decimal GetPrice()
    {
        return _product.GetPrice();
    }
}


class DiscountDecorator : ProductDecorator
{
    private decimal _discount;

    public DiscountDecorator(IProduct product, decimal discount) : base(product)
    {
        _discount = discount;
    }

    public override decimal GetPrice()
    {
        decimal originalPrice = base.GetPrice();
        return originalPrice - (_discount * originalPrice);
    }
}


interface IPriceCalculationStrategy
{
    decimal CalculatePrice(IProduct product);
}


class SimplePriceCalculationStrategy : IPriceCalculationStrategy
{
    public decimal CalculatePrice(IProduct product)
    {
        return product.GetPrice();
    }
}

class Program
{
    static void Main()
    {
      
        IProduct product1 = new Product("Producto 1", 100.0m);
        IProduct product2 = new ProductAdapter(new Product("Producto 2", 50.0m));

        
        IProduct discountedProduct = new DiscountDecorator(product1, 0.1m);
        IProduct discountedProduct2 = new DiscountDecorator(product2, 0.2m);

       
        IPriceCalculationStrategy strategy = new SimplePriceCalculationStrategy();
        Console.WriteLine("Precio Producto 1: " + strategy.CalculatePrice(product1));
        Console.WriteLine("Precio Producto 2: " + strategy.CalculatePrice(product2));

        strategy = new SimplePriceCalculationStrategy(); 
        Console.WriteLine("Precio Producto 1 con descuento: " + strategy.CalculatePrice(discountedProduct));
        Console.WriteLine("Precio Producto 2 con descuento: " + strategy.CalculatePrice(discountedProduct2));
        Console.ReadKey();
    }
}
