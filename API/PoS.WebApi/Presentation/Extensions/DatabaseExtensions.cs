using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Infrastructure.Persistence;

namespace PoS.WebApi.Presentation.Extensions;

public static class DatabaseExtensions
{
    public static IApplicationBuilder ConfigureSqliteDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

        var connection = dbContext.Database.GetDbConnection();
        connection.Open();
        
        using var command = connection.CreateCommand();
        command.CommandText = "PRAGMA journal_mode = DELETE;";
        command.ExecuteNonQuery();

        return app;
    }
}