using ASPNetExapp.Services;
using ASPNetExapp.Data;
using Microsoft.EntityFrameworkCore;

// Створюємо екземпляр класу WebApplication, який дозволяє створювати веб-додатки ASP.NET Core
var builder = WebApplication.CreateBuilder(args);

// Додаємо підключення до бази даних
var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Додаємо підтримку контролерів
builder.Services.AddControllers();
// Додаємо підтримку Swagger
builder.Services.AddSwaggerGen();
// Додаємо кастомний сервіс
builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers(); // Важливо для роботи контролерів! Це по суті налаштування маршрутизації

app.Run();
