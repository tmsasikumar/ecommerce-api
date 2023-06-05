using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Products.Api.Data;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var builderConfiguration = builder.Configuration;
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddDbContext<EcommerceContext>(options =>
{
    options.UseNpgsql(builderConfiguration.GetConnectionString("DefaultConnection"));
});
services.AddAutoMapper(Assembly.GetExecutingAssembly());
services.AddOpenTelemetryTracing((b) =>
{
    b.SetResourceBuilder(
            ResourceBuilder.CreateDefault()
                .AddService(builder.Environment.ApplicationName)
        ).AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddOtlpExporter(opts =>
        {
            opts.Endpoint = new Uri("http://localhost:4317");
        });
});

var app = builder.Build();

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
