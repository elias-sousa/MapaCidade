using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapaCidade.Apresentacao.MVC
{
    /// <summary>
    /// Represents the current application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry point to the application.
        /// </summary>
        /// <param name="args">The arguments provides at start-up, if any.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Builds a new web host for the application.
        /// </summary>
        /// <param name="args">The command-line arguments, if any.</param>
        /// <returns>A new <see cref="IHostBuilder"> host builder</see>.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseIISIntegration();
                    webBuilder.UseStartup<Startup>();
                });
    }
}
