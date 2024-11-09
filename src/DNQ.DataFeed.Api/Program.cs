using DNQ.DataFeed.Api.Middlewares;
using DNQ.DataFeed.Api.Startup;
using DNQ.DataFeed.Application;
using DNQ.DataFeed.Domain;
using DNQ.DataFeed.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigAppSettings();

// Add services to the container.
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddDomain();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

/* Extend ExceptionHandlerMiddleware instead of re-writing */
app.UseExceptionHandler(err => err.UseCustomErrors(app.Environment));

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
