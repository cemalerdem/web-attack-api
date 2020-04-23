using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Notion.Api
{
    public class Program
    {
        public static void Main(string[] args)
        => CreateHostBuilder(args).Build().Run();


       // EF Core uses this method at design time to access the DbContext
    public static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
                webBuilder => webBuilder.UseStartup<Startup>()
                .UseSentry("https://0df94822be474a71982ebf5e5205439a@o382518.ingest.sentry.io/5211477"));
    }
}
