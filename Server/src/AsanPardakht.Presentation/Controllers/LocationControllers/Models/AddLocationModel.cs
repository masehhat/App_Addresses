namespace AsanPardakht.Presentation.Controllers.LocationControllers.Models;

public record AddLocationModel : ModifyLocationModel
{
    public int UserId { get; init; }
}