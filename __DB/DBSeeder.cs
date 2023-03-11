using Microsoft.EntityFrameworkCore;
using Bogus;

namespace TaskManagement.DB;

public static class DBSeeder
{
    private static TaskManagementContext _db;
    private static ILogger<TaskManagementContext> _logger;
    private static Faker _faker = new("en");

    private static async Task Seed()
    {
        await _db.Database.MigrateAsync();
        // seed db here ...
    }
    public static async Task UseDbSeeder(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));
        using var scope = app.ApplicationServices.CreateScope();
        try
        {
            _logger = scope.ServiceProvider.GetRequiredService<ILogger<TaskManagementContext>>();
            _db = scope.ServiceProvider.GetRequiredService<TaskManagementContext>();
            await Seed();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}
