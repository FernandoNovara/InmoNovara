using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InmoNovara
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateKestrel(args).Build().Run();
        }



        // public static IHostBuilder CreateHostBuilder(string[] args) =>
        //     Host.CreateDefaultBuilder(args)
        //         .ConfigureWebHostDefaults(webBuilder =>
        //         {
        //             webBuilder.UseStartup<Startup>();
        //             // builder.WebHost.ConfigureKestrel(serverOptions =>
        //             // {
        //             // serverOptions.ListenAnyIP(5000);
        //             // });
        //         });
                
        
        public static IWebHostBuilder CreateKestrel(string[] args)
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.Build();
			var host = new WebHostBuilder()
				.UseConfiguration(config)
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				//.UseUrls("http://localhost:5000", "https://localhost:5001")//permite escuchar SOLO peticiones locales
				.UseUrls("http://*:5000", "https://*:5001")//permite escuchar peticiones locales y remotas
				.UseIISIntegration()
				.UseStartup<Startup>();
			return host;
		}      

    }

    

    
    

}
