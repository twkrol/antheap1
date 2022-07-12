using antheap1.Data;

public static class Extensions
{
    public static void CreateDbIfNotExists(this IHost host)
    {
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<MyDbContext>();
                context.Database.EnsureCreated();
                // if (context.Database.EnsureCreated())
                // {
                //     DbInitializer.Initialize(context);
                // }
            }
        }
    }
}