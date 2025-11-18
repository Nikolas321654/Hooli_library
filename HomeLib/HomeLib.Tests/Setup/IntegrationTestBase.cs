using HomeLib.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Xunit;

public abstract class IntegrationTestBase : IAsyncLifetime
{
    private IDbContextTransaction _transaction;

    private const string TestConnectionString =
        "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=mysecretpassword";

    private HomeLibDbContext Context { get; set; }

    public async Task InitializeAsync()
    {
        var options = new DbContextOptionsBuilder<HomeLibDbContext>()
            .UseNpgsql(TestConnectionString)
            .Options;

        Context = new HomeLibDbContext(options);

        await Context.Database.EnsureCreatedAsync();

        _transaction = await Context.Database.BeginTransactionAsync();

        await SeedBaseDataAsync(Context);
    }

    public async Task DisposeAsync()
    {
        await _transaction.RollbackAsync();

        await _transaction.DisposeAsync();
        Context.Dispose();
    }

    protected virtual Task SeedBaseDataAsync(HomeLibDbContext context) => Task.CompletedTask;
}
