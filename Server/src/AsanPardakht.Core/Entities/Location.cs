using AsanPardakht.Core.Utilities;

namespace AsanPardakht.Core.Entities;

public class Location
{
    private Location()
    {
    }

    public Location(int userId, string province, string city, string address)
    {
        UserId = userId;
        Province = province;
        City = city;
        Address = address;
        CreatedAt = DateTime.UtcNow.ToUnixTime();
    }

    public int Id { get; }
    public int UserId { get; }

    private string _province;

    public string Province
    {
        get => _province;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(Province));

            if (value.Length > 50)
                throw new ArgumentOutOfRangeException(nameof(Province));

            _province = value;
        }
    }

    private string _city;

    public string City
    {
        get => _city;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(City));

            if (value.Length > 50)
                throw new ArgumentOutOfRangeException(nameof(City));

            _city = value;
        }
    }

    private string _address;

    public string Address
    {
        get => _address;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(Address));

            _address = value;
        }
    }

    public int CreatedAt { get; }

    public void Modify(string province, string city, string address)
    {
        Province = province;
        City = city;
        Address = address;
    }
}