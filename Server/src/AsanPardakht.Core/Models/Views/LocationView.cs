namespace AsanPardakht.Core.Models.Views;

public record LocationView
{
    public int Id { get; init; }
    public string CombinedAddress { get; init; }    
    public int CreatedAt { get; init; }
}