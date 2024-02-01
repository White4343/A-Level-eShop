namespace Basket.API.Data
{
    public class DbInnit
    {
        public static async Task InnitAsync(AppDbContext context)
        {
            await context.Database.EnsureCreatedAsync();
        }
    }
}
