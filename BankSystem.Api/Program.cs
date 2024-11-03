using BankSystem.Api.FluentValidations.ClientValidations;
using BankSystem.Api.Mapping;
using BankSystem.Api.Services.Implementations;
using BankSystem.Api.Services.Interfaces;
using BankSystem.Application.FluentValidations.ClientValidations;
using BankSystem.Application.FluentValidations.EmployeeValidations;
using BankSystem.Data;
using BankSystem.Data.Storage.Implementations;
using BankSystem.Data.Storage.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BankSystemDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IClientStorage, ClientStorage>();
builder.Services.AddScoped<IEmployeeStorage, EmployeeStorage>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddAutoMapper(typeof(ClientMappingProfile), typeof(EmployeeMappingProfile));
builder.Services.AddFluentValidation(config =>
{
    config.RegisterValidatorsFromAssemblyContaining<CreateClientRequestValidator>();
    config.RegisterValidatorsFromAssemblyContaining<UpdateClientRequestValidator>();
    config.RegisterValidatorsFromAssemblyContaining<CreateEmployeeRequestValidator>();
    config.RegisterValidatorsFromAssemblyContaining<UpdateEmployeeRequestValidator>();
});
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();


app.Run();

