using AsanPardakht.Consumer.App;
using Hangfire;

GlobalConfiguration.Configuration.UseSqlServerStorage(Configs.AsanpardakhtDbConnectionString);

LocationService LocationService = new();
RecurringJob.AddOrUpdate("FetchTehranAddressesJob", () => LocationService.GetTehranLocations(), "*/10 * * * *");

using (var server = new BackgroundJobServer())
{
    Console.WriteLine("Hangfire Server started. Press any key to exit...");
    Console.ReadKey();
}