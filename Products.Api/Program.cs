using System.Reflection;
using Microsoft.EntityFrameworkCore;
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

var honeycombOptions = builder.Configuration.GetHoneycombOptions();
services.AddOpenTelemetry().WithTracing(otelBuilder =>
{
    otelBuilder
        .AddHoneycomb(honeycombOptions)
        .AddCommonInstrumentations();
});
builder.Services.AddSingleton(TracerProvider.Default.GetTracer(honeycombOptions.ServiceName));

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
