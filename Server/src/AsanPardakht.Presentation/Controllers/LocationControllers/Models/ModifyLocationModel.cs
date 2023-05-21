namespace AsanPardakht.Presentation.Controllers.LocationControllers.Models;

public record ModifyLocationModel
{    
    public string Province { get; init; }
    public string City { get; init; }
    public string Address { get; init; }
}
