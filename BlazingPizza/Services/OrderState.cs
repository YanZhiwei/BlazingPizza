using BlazingPizza.Repository.Entities;
using BlazingPizza.Shared;

namespace BlazingPizza.Services;

public class OrderState
{
    public OrderState()
    {
        Order = new Order
        {
            DeliveryAddress = new Address()
            {
                City = "test",
                Name = "name",
                Line1 = "test",
                Line2 = "test",
                Region = "test",
                PostalCode = "test"
            },
            UserId = Guid.NewGuid().ToString("N")
        };
    }
    public bool ShowingConfigureDialog { get; private set; }
    public Pizza ConfiguringPizza { get; private set; }
    public Order Order { get; private set; }

    public void ShowConfigurePizzaDialog(PizzaSpecial special)
    {
        ConfiguringPizza = new Pizza
        {
            Special = special,
            SpecialId = special.Id,
            Size = Pizza.DefaultSize,
            Toppings = new List<PizzaTopping>()
        };

        ShowingConfigureDialog = true;
    }

    public void CancelConfigurePizzaDialog()
    {
        ConfiguringPizza = null;
        ShowingConfigureDialog = false;
    }

    public void ConfirmConfigurePizzaDialog()
    {
        Order.Pizzas.Add(ConfiguringPizza);
        ConfiguringPizza = null;
        ShowingConfigureDialog = false;
    }
    public void ResetOrder()
    {
        Order = new Order
        {
            DeliveryAddress = new Address()
            {
                City = "test",
                Name = "name",
                Line1 = "test",
                Line2 = "test",
                Region = "test",
                PostalCode = "test"
            },
            UserId = Guid.NewGuid().ToString("N")
        };
    }
    public void RemoveConfiguredPizza(Pizza pizza)
    {
        Order.Pizzas.Remove(pizza);
    }
}
