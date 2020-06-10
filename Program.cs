using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Maintel.DotNetCore.Logging;

namespace Maintel.Icon.Portal.Sync.HighlightAPI
{
    /// <summary>
    /// The main program class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry point for the program class
        /// </summary>
        /// <param name="args">Start up arguments</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Configures the web service and logging
        /// </summary>
        /// <param name="args">Start up arguments</param>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    //logging.AddConsoleLogger();
                    //logging.AddCallmediaLogger();
                    //logging.AddEventLogLogger();
                    logging.AddFileLogger();
                    //logging.AddDatabaseLogger();
                    //logging.AddSyslogLogger();
                    //logging.AddFileLogger(options => {  });
                });
    }
}
