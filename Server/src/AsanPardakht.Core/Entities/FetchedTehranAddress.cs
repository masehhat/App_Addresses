namespace AsanPardakht.Core.Entities;

public class FetchedTehranAddress
{
    public FetchedTehranAddress(string address, int originalCreatedAt)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentNullException(nameof(address));

        Address = address;
        OriginalCreatedAt = originalCreatedAt;
    }

    public int Id { get; }
    public string Address { get; }
    public int OriginalCreatedAt { get; }
}