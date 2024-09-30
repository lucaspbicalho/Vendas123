using Microsoft.EntityFrameworkCore;
using Vendas123.Infrastructure.Contexts;
using Vendas123.Infrastructure.Repositories;
using Vendas123.Services.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

//configure Fake context 
builder.Services.AddSingleton<FakeContext>();
//configure InMemory context 
builder.Services.AddDbContext<VendasDbContext>(db => db.UseInMemoryDatabase("VendasDb"));
// Repository
builder.Services.AddScoped<IVendaRepository, VendaRepository>();
// Services
builder.Services.AddScoped<VendaService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
