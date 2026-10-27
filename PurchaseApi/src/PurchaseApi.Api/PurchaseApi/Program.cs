using MassTransit;
using PurchaseApi.Application.UseCases;
using PurchaseApi.Infrastructure;
using PurchaseApi.Infrastructure.Migrations;
using PurchaseApi.Messaging;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(o =>
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration, x =>
{
    x.AddConsumer<StockProcessedConsumer>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

const string WebCorsPolicy = "web";

builder.Services.AddCors(options =>
    options.AddPolicy(WebCorsPolicy, policy =>
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()));


var app = builder.Build();

app.UseCors(WebCorsPolicy);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await DataBaseMigration.MigrateDataBase(app.Services);

app.Run();
