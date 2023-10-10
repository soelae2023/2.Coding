namespace SimpleApISystem.Models;

public partial class PayLoad
{
    public int Id { get; set; }

    public decimal? Temperature { get; set; }

    public int? Humidity { get; set; }

    public bool? Occupancy { get; set; }
}
