using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace NBandServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Module.Log($"================程序启动================");
            
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                                         .AddJsonFile("host.json")
                                         .Build();
            return WebHost.CreateDefaultBuilder(args)
                            .UseConfiguration(configuration)
                            .UseStartup<Startup>();
        }           
    }
}
