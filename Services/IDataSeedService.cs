namespace NadsTech.Services;

public interface IDataSeedService
{
    Task SeedArticlesAsync();
    Task SeedCommentsAsync();
    Task SeedReactionsAsync();
    Task SeedAllDataAsync();
} 