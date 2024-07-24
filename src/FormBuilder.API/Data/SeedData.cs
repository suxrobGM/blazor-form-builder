using FormBuilder.API.Entities;

namespace FormBuilder.API.Data;

public class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<SeedData>>();
        await using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        // Look for any existing data
        if (context.LovMaster.Any())
        {
            logger.LogInformation("Database already seeded with LOV items");
            return;
        }

        logger.LogInformation("Seeding database");
        var lovData = GenerateLovData(200);
        
        context.LovMaster.AddRange(lovData);
        await context.SaveChangesAsync();
        logger.LogInformation("Database seeded with {Count} LOV items", lovData.Count);
    }

    /// <summary>
    /// Generates a list of LOV items with random ListId and ListValue
    /// </summary>
    /// <param name="count">The number of LOV items to generate</param>
    /// <returns>A list of LOV items with random ListId and ListValue</returns>
    private static List<Lov> GenerateLovData(int count)
    {
        var random = new Random();
        var lovData = new List<Lov>();

        for (var i = 0; i < count; i++)
        {
            lovData.Add(new Lov
            {
                ListId = random.Next(1, 10), // Random ListId between 1 and 10
                ListValue = $"Value {i + 1}"
            });
        }

        return lovData;
    }
}
