using BankSystem.App.Services;
using BankSystem.App.Services.Implementations;
using BankSystem.Data;
using BankSystem.Data.Storage.Implementations;
using BankSystem.Data.Storage.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DataBase");
builder.Services.AddDbContext<BankSystemDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

// Регистрация сервисов и хранилищ
builder.Services.AddScoped<IClientStorage, ClientStorage>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<IEmployeeStorage, EmployeeStorage>();

builder.Services.AddHttpClient<CurrencyService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();