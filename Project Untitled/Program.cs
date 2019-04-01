using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Project_Untitled
{
    public class Program
    {
        public static void Main(string[] args)
        {
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
