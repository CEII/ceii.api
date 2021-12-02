// MinimalApi -> net6.0
using Ceii.Api.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add DbContext to builder

/// <summary>
/// Options sets connection string from env variables, then sets assembly for project that should contain migrations
/// and contains DbContext class
/// </summary>
builder.Services.AddDbContext<CeiiDbContext>(opt => 
    opt.UseSqlServer(builder.Configuration.GetConnectionString("CeiiDb"),
        b => b.MigrationsAssembly("Ceii.Api.Data")
));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

// SetUp Swagger for Dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
