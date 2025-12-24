using dotenv.net;
using Microsoft.EntityFrameworkCore;

namespace api.DbContext;

public static class ContextFactory
{
    private static DbContextOptions? _options;

    public static TrackListDbContext CreateNew()
    {
        var env = DotEnv.Read();
        _options ??= new DbContextOptionsBuilder<TrackListDbContext>()
            .UseLazyLoadingProxies()
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine)
            .UseNpgsql(env["CONNECTION_STRING"])
            .Options;

        return new TrackListDbContext(_options);
    }
}