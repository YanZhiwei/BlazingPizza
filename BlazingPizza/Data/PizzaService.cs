namespace BlazingPizza.Data;

public class PizzaService
{
    public async Task<Pizza[]> GetPizzasAsync()
    {
        Pizza[] pizzas =
        {
            new()
            {
                Name = "The Baconatorizor", PizzaId = 1, Description = "It has EVERY kind of bacon",
                Price = 11.11m
            },
            new()
            {
                Name = "Buffalo chicken", PizzaId = 2,
                Description = "Spicy chicken, hot sauce, and blue cheese, guaranteed to warm you up",
                Price = 12.22m
            },
            new()
            {
                Name = "Veggie Delight", PizzaId = 3, Description = "It's like salad, but on a pizza",
                Price = 13.33m
            },
            new()
            {
                Name = "Margherita", PizzaId = 4, Description = "Traditional Italian pizza with tomatoes and basil",
                Price = 14.44m
            },
            new()
            {
                Name = "Basic Cheese Pizza", PizzaId = 5,
                Description = "It's cheesy and delicious. Why wouldn't you want one?",
                Price = 15.55m
            },
            new()
            {
                Name = "Classic pepperoni", PizzaId = 6,
                Price = 16.66m,
                Description = "It's the pizza you grew up with, but Blazing hot!"
            }
        };
        return await Task.FromResult(pizzas);
    }
}