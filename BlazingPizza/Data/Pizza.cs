using BlazingPizza.Model;
using BlazingPizza.Services;

namespace BlazingPizza.Data;

public class Pizza
{
    public const int DefaultSize = 12;
    public const int MinimumSize = 9;
    public const int MaximumSize = 17;
    public int PizzaId { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public bool Vegetarian { get; set; }

    public bool Vegan { get; set; }
    public PizzaSpecial Special { get; set; }
    public int SpecialId { get; set; }
    public object Size { get; set; }
    public List<PizzaTopping> Toppings { get; set; } = new();

    public decimal GetBasePrice()
    {
        if (Special == null)
            throw new NullReferenceException($"{nameof(Special)} was null when calculating Base Price.");
        return (decimal)Size / DefaultSize * Special.BasePrice;
    }

    public decimal GetTotalPrice()
    {
        if (Toppings.Any(t => t.Topping is null))
            throw new NullReferenceException($"{nameof(Toppings)} contained null when calculating the Total Price.");
        return GetBasePrice() + Toppings.Sum(t => t.Topping!.Price);
    }

    public string GetFormattedTotalPrice()
    {
        return GetTotalPrice().ToString("0.00");
    }
}