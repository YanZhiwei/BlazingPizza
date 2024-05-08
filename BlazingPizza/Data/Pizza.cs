using BlazingPizza.Model;
using BlazingPizza.Services;

namespace BlazingPizza.Data;

public class Pizza
{
    public int PizzaId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public bool Vegetarian { get; set; }

    public bool Vegan { get; set; }
    public PizzaSpecial Special { get; set; }
    public int SpecialId { get; set; }
    public static object DefaultSize { get; set; }
    public object Size { get; set; }
    public List<PizzaTopping> Toppings { get; set; }
}