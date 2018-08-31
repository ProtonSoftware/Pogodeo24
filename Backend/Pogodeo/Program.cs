using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Dna;
using Dna.AspNet;

namespace Pogodeo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                // Add Dna Framework
                .UseDnaFramework(construct =>
                {
                    // Configure framework

                    // Add file logger
                    construct.AddFileLogger();
                })
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();
    }
}
