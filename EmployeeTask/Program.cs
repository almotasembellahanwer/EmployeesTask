using EmployeeTask.Core.Mappers;
using EmployeeTask.Infrastructure;
using EmployeeTask.Core;
using EmployeeTask.Middlewares;
using FluentValidation.AspNetCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfrastructures(builder.Configuration);
builder.Services.AddCore();

builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseErrorHandlingMiddleware();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
