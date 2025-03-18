using Microsoft.AspNetCore.Cors;
using Microsoft.OpenApi.Models;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ghosn API", Version = "v1" });
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder
            .WithOrigins(
                "http://localhost:5173",
                "http://mostafashraief.bsite.net",
                "https://mostafashraief.bsite.net"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ghosn API v1"));
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowOrigin");
app.UseAuthorization();

app.MapControllers();

// Add a minimal API endpoint for testing (without WithOpenApi)
app.MapGet("/api/test", () => new { Message = "Backend is connected!" })
    .WithName("GetTestMessage");

app.Run();