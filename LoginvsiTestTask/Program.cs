namespace LoginvsiTestTask;

using Microsoft.AspNetCore;

public static class Program
{
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Run();
    }

    public static IWebHost CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddDebug();
                logging.AddConsole();
            })
            .UseStartup<Startup.Startup>()
//            .UseUrls("http://0.0.0.0:5005")
            .Build();
}