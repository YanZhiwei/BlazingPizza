using System.Text.Json.Serialization;
using BlazingPizza.Data;
using BlazingPizza.Model;
using static Duende.IdentityServer.Models.IdentityResources;

namespace BlazingPizza.Services;

public class OrderState
{
    public bool ShowingConfigureDialog { get; private set; }
    public Pizza ConfiguringPizza { get; private set; }
    public Order Order { get; } = new();

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
}

public class PizzaTopping
{
    public Topping? Topping { get; set; }

    public int ToppingId { get; set; }

    public int PizzaId { get; set; }
}

public class Topping
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string GetFormattedPrice()
    {
        return Price.ToString("0.00");
    }
}

public class Order
{
    public int OrderId { get; set; }

    // Set by the server during POST
    public string? UserId { get; set; }

    public DateTime CreatedTime { get; set; }

    public Address DeliveryAddress { get; set; } = new();

    // Set by server during POST
    public LatLong? DeliveryLocation { get; set; }

    public List<Pizza> Pizzas { get; set; } = new();

    public decimal GetTotalPrice()
    {
        return Pizzas.Sum(p => p.GetTotalPrice());
    }

    public string GetFormattedTotalPrice()
    {
        return GetTotalPrice().ToString("0.00");
    }
}

[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(Order))]
[JsonSerializable(typeof(OrderWithStatus))]
[JsonSerializable(typeof(List<OrderWithStatus>))]
[JsonSerializable(typeof(Pizza))]
[JsonSerializable(typeof(List<PizzaSpecial>))]
[JsonSerializable(typeof(List<Topping>))]
[JsonSerializable(typeof(Topping))]
[JsonSerializable(typeof(Dictionary<string, string>))]
public partial class OrderContext : JsonSerializerContext
{
}

public class LatLong
{
    public LatLong()
    {
    }

    public LatLong(double latitude, double longitude) : this()
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public static LatLong Interpolate(LatLong start, LatLong end, double proportion)
    {
        // The Earth is flat, right? So no need for spherical interpolation.
        return new LatLong(
            start.Latitude + (end.Latitude - start.Latitude) * proportion,
            start.Longitude + (end.Longitude - start.Longitude) * proportion);
    }
}

public class OrderWithStatus
{
    public static readonly TimeSpan PreparationDuration = TimeSpan.FromSeconds(10);

    public static readonly TimeSpan
        DeliveryDuration = TimeSpan.FromMinutes(1); // Unrealistic, but more interesting to watch

    // Set from DB
    public Order Order { get; set; } = null!;

    // Set from Order
    public string StatusText { get; set; } = null!;

    public bool IsDelivered => StatusText == "Delivered";

    public List<Marker> MapMarkers { get; set; } = null!;

    public static OrderWithStatus FromOrder(Order order)
    {
        ArgumentNullException.ThrowIfNull(order.DeliveryLocation);
        // To simulate a real backend process, we fake status updates based on the amount
        // of time since the order was placed
        string statusText;
        List<Marker> mapMarkers;
        var dispatchTime = order.CreatedTime.Add(PreparationDuration);

        if (DateTime.Now < dispatchTime)
        {
            statusText = "Preparing";
            mapMarkers = new List<Marker>
            {
                ToMapMarker("You", order.DeliveryLocation, true)
            };
        }
        else if (DateTime.Now < dispatchTime + DeliveryDuration)
        {
            statusText = "Out for delivery";

            var startPosition = ComputeStartPosition(order);
            var proportionOfDeliveryCompleted = Math.Min(1,
                (DateTime.Now - dispatchTime).TotalMilliseconds / DeliveryDuration.TotalMilliseconds);
            var driverPosition =
                LatLong.Interpolate(startPosition, order.DeliveryLocation, proportionOfDeliveryCompleted);
            mapMarkers = new List<Marker>
            {
                ToMapMarker("You", order.DeliveryLocation),
                ToMapMarker("Driver", driverPosition, true)
            };
        }
        else
        {
            statusText = "Delivered";
            mapMarkers = new List<Marker>
            {
                ToMapMarker("Delivery location", order.DeliveryLocation, true)
            };
        }

        return new OrderWithStatus
        {
            Order = order,
            StatusText = statusText,
            MapMarkers = mapMarkers
        };
    }

    private static LatLong ComputeStartPosition(Order order)
    {
        ArgumentNullException.ThrowIfNull(order.DeliveryLocation);
        // Random but deterministic based on order ID
        var rng = new Random(order.OrderId);
        var distance = 0.01 + rng.NextDouble() * 0.02;
        var angle = rng.NextDouble() * Math.PI * 2;
        var offset = (distance * Math.Cos(angle), distance * Math.Sin(angle));
        return new LatLong(order.DeliveryLocation.Latitude + offset.Item1,
            order.DeliveryLocation.Longitude + offset.Item2);
    }

    private static Marker ToMapMarker(string description, LatLong coords, bool showPopup = false)
    {
        return new Marker()
            { Description = description, X = coords.Longitude, Y = coords.Latitude, ShowPopup = showPopup };
    }
}

public class Marker
{
    public string Description { get; set; } = string.Empty;

    public double X { get; set; }

    public double Y { get; set; }

    public bool ShowPopup { get; set; }
}