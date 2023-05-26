using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Products.Api.Data;

var builder = WebApplication.CreateBuilder(args);

var serviceCollection = builder.Services;
var builderConfiguration = builder.Configuration;
serviceCollection.AddControllers();
serviceCollection.AddEndpointsApiExplorer();
serviceCollection.AddSwaggerGen();
serviceCollection.AddDbContext<EcommerceContext>(options =>
{
    options.UseNpgsql(builderConfiguration.GetConnectionString("DefaultConnection"));
});
serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());

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
