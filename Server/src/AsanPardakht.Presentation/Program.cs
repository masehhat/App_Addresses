using AsanPardakht.Core.Data;
using AsanPardakht.Core.Mappings;
using AsanPardakht.Core.Middlewares;
using AsanPardakht.Core.Services;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(LocationMappingProfile).Assembly);
builder.Services.AddScoped<ILocationService, LocationService>();

builder.Services.AddDbContext<AsanPardakhtDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AsanPardakhtDbConnection"))
    .LogTo(message => Debug.WriteLine(message), Microsoft.Extensions.Logging.LogLevel.Information);
    options.EnableSensitiveDataLogging(true);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ApiExceptionHandler>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
