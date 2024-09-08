using ACIDemo.Data;
using Microsoft.EntityFrameworkCore;

namespace ACIDemo.Extensions;

public static class Migration
{
    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
        try
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
            // migrate the database automatically 
            dbContext.Database.MigrateAsync();

            return app;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        
    }
}
