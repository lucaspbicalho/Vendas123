using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Vendas123.Domain.Entites;
using Vendas123.Infrastructure.Contexts;
using Vendas123.Tests.Fixtures;

namespace Vendas123.Tests.Controllers
{
    public class WebApplicationFactoryFixture : IAsyncLifetime
    {
        private const string _connectionString = @$"Server=(localdb)\.;Database=VendasDb;Trusted_Connection=True";
        private WebApplicationFactory<Program> _factory;
        public HttpClient _client { get; private set; }

        public WebApplicationFactoryFixture()
        {
            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(webBuilder =>
            {
                webBuilder.ConfigureTestServices(services =>
                {
                    services.RemoveAll(typeof(DbContextOptions<VendasDbContext>));
                    services.AddDbContext<VendasDbContext>(opt =>
                    {
                        opt.UseInMemoryDatabase(_connectionString);
                    });

                });
            });
            _client = _factory.CreateClient();
        }

        async Task IAsyncLifetime.InitializeAsync()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var scopService = scope.ServiceProvider;
                var dbContext = scopService.GetService<VendasDbContext>();

                await dbContext.Database.EnsureCreatedAsync();

                await dbContext.Vendas.AddAsync(DataFixture.GetVenda());
                await dbContext.SaveChangesAsync();
            }
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var scopService = scope.ServiceProvider;
                var dbContext = scopService.GetService<VendasDbContext>();

                await dbContext.Database.EnsureDeletedAsync();
            }
        }
    }
}