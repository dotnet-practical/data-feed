using DNQ.DataFeed.Api.Middlewares;
using DNQ.DataFeed.Application;
using DNQ.DataFeed.Domain;
using DNQ.DataFeed.Persistence;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddDomain();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.ConfigureCustomModelValidationResponse();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/* Extend ExceptionHandlerMiddleware instead of re-writing */
app.UseExceptionHandler(err => err.UseCustomErrors(app.Environment));

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
