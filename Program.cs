var builder = WebApplication.CreateBuilder(args);

// Конфигурация для Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Подключение PostgreSQL
builder.Services.AddDbContext<CountryCatalogContext>(options =>
    options.UseNpgsql(builder.Configuration["CONNECTION_STRING"]));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

// Автомиграции
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CountryCatalogContext>();
    db.Database.Migrate();
}

app.Run();