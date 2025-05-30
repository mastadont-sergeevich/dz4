using Microsoft.EntityFrameworkCore;
using CountryCatalogAPI.Data;
using CountryCatalogAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Конфигурация сервисов
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Country Catalog API", Version = "v1" });
});

// Подключение БД Supabase
builder.Services.AddDbContext<CountryCatalogContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    #if DEBUG
    options.LogTo(Console.WriteLine, LogLevel.Information); // Логирование SQL в консоль
    #endif
});

var app = builder.Build();

// Настройка конвейера запросов
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Country API v1");
        c.RoutePrefix = "swagger";
    });
}

// Автоматическое применение миграций при запуске
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CountryCatalogContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();