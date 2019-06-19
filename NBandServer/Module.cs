using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
namespace NBandServer
{
    public class Module
    {
        public static int serverPort = 5002;
        public static readonly string version = "1.0.2";
        public static ServiceProvider serviceProvider;
        public static string mysqlConnstr = "";
        public static void Init(IConfiguration Configuration, ServiceProvider serviceProvider)
        {
            Console.Title = "NBandServer " + version;            
            Module.serviceProvider = serviceProvider;
        
            serverPort = int.Parse(Configuration.GetSection("Config").GetSection("ServerPort").Value);
            mysqlConnstr = Configuration.GetConnectionString("MysqlConnection");
            Start();
        }
        public static void Start()
        {
           
        }
        public static void Stop()
        {
        }
        public static void Log(string str)
        {
            Console.WriteLine(DateTime.Now.ToString("[HH:mm:ss] ") + str);          
        }
    }
}
