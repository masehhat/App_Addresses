using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsanPardakht.Core.Entities;

public class User
{
    private User()
    {

    }

    public User(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentNullException(nameof(username));

        if (username.Length > 250)
            throw new ArgumentOutOfRangeException(nameof(username));

        Username = username;
    }

    public int Id { get; }
    public string Username { get; }
    public ICollection<Location> Locations { get; private set; }
}
