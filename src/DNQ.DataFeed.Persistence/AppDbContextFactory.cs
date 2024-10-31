using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DNQ.DataFeed.Persistence;

/*
**  Using for run add migrations  
*/
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var enviroment = GetEnvironmentFromArgs(args);
        var directory = GetDirectory();
        var configuration = BuildConfiguration(directory, enviroment);

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        Console.WriteLine($"directory: {directory} - enviroment: {enviroment} - connectionString: {connectionString}");

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        return new AppDbContext(optionsBuilder.Options);
    }

    private string GetEnvironmentFromArgs(string[] args)
    {
        // Default environment
        string environment = "Development";

        // Check if --environment is passed in args and extract its value
        if (args != null)
        {
            var envArg = args.FirstOrDefault(arg => arg.StartsWith("--environment", StringComparison.OrdinalIgnoreCase));
            if (envArg != null)
            {
                var envArgParts = envArg.Split('=', 2);
                if (envArgParts.Length == 2)
                {
                    environment = envArgParts[1];
                }
            }
        }

        return environment;
    }

    public IConfiguration BuildConfiguration(string basePath, string environment)
    {
        return new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .Build();
    }

    public string GetDirectory()
    {
        var currentDirectory = Directory.GetCurrentDirectory(); // DNQ.DataFeed.Persistence

        var parentDirectoryInfor = Directory.GetParent(currentDirectory);

        if (parentDirectoryInfor == null)
        {
            throw new InvalidOperationException("Cannot determine the parent directory of the current directory.");
        }

        var parentDirectory = parentDirectoryInfor.FullName; //src

        string apiDirectory = Path.Combine(parentDirectory, "DNQ.DataFeed.Api");

        return apiDirectory;
    }
}
