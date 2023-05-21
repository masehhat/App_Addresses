using Microsoft.EntityFrameworkCore;

namespace AsanPardakht.Consumer.App;

public class LocationService
{
    public async Task GetTehranLocations()
    {
        DbContextOptions<AsanpardakhtDbContext> dbContextOptions = new DbContextOptionsBuilder<AsanpardakhtDbContext>()
            .UseSqlServer(Configs.AsanpardakhtDbConnectionString).Options;

        using AsanpardakhtDbContext context = new(dbContextOptions);

        HttpClient client = new()
        {
            BaseAddress = new Uri(Configs.AsanPardakhtBaseUrl)
        };

        string url = "api/location?city=تهران";

        int lastFetchOriginalDateTime = await context.FetchedTehranAddresses.OrderByDescending(x => x.OriginalCreatedAt)
            .Select(x => x.OriginalCreatedAt)
            .FirstOrDefaultAsync();

        //One second after last fetched original createdAt
        if (lastFetchOriginalDateTime != default)
            url += $"fromDateTime={lastFetchOriginalDateTime + 1}";

        HttpResponseMessage response = await client.GetAsync(url);
        string strResponse = await response.Content.ReadAsStringAsync();

        ApiResponseStructure<PagedData<LocationView>> result = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponseStructure<PagedData<LocationView>>>(strResponse);

        if (!result.Data.Items.Any())
            return;

        FetchedTehranAddress[] fetchedTehranAddresses = result.Data.Items.Select(x => new FetchedTehranAddress(x.CombinedAddress, x.CreatedAt)).ToArray();
        context.FetchedTehranAddresses.AddRange(fetchedTehranAddresses);
        await context.SaveChangesAsync();

    }
}