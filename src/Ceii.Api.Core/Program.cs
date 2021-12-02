// MinimalApi -> net6.0
using Ceii.Api.Core.Injections;
using Ceii.Api.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. Also enable serialization
builder.Services.AddControllers().AddNewtonsoftJson(o => 
{
    o.SerializerSettings.Converters.Add(new StringEnumConverter
    {
        NamingStrategy = new CamelCaseNamingStrategy
        {
            OverrideSpecifiedNames = false
        }
    });
});

// Add DbContext to builder
builder.Services.AddDbContext<CeiiDbContext>(opt => 
    opt.UseSqlServer(builder.Configuration.GetConnectionString("CeiiDb"),
        b => b.MigrationsAssembly("Ceii.Api.Data")
));

// Enable CORS
builder.Services.AddCors(options => {
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
    );
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CEII API", Version = "v1" });
});
builder.Services.AddSwaggerGenNewtonsoftSupport();

// Add Services
builder.Services.AddTransientServices();

var app = builder.Build();

// Configure the HTTP request pipeline.

// SetUp Swagger for Dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
