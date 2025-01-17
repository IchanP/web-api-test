using test_api.services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

string? httpClientName = builder.Configuration["WeatherClientName"];
ArgumentException.ThrowIfNullOrEmpty(httpClientName);
string? weatherFetcherBaseUrl = builder.Configuration["WeatherFetcherUrl"];
ArgumentException.ThrowIfNullOrEmpty(weatherFetcherBaseUrl);

// Configure HttpClient with base address
builder.Services.AddHttpClient<WeatherFetcher>(httpClientName, client =>
{
    client.BaseAddress = new Uri(weatherFetcherBaseUrl);
});

builder.Services.AddSingleton<IWeatherFetcher, WeatherFetcher>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();