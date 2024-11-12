using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;

namespace ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var iniFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini");
                if (!File.Exists(iniFilePath))
                    throw new ApplicationException($"No .ini file found. Tried the following path - {iniFilePath}");

                var clientIni = new IniFile(iniFilePath, "ClientSettings");
                var url = clientIni.Read("Url");
                var hubConnUrl = $"{url}/OrderHub";
                
                Console.WriteLine($"Build hub connection {hubConnUrl}");
                var conn = new HubConnectionBuilder()
                    .WithUrl(hubConnUrl, transports: (HttpTransportType.LongPolling | HttpTransportType.ServerSentEvents) & ~HttpTransportType.WebSockets,
                    (opts) =>
                    {
                        opts.HttpMessageHandlerFactory = (message) =>
                        {
                            if (message is HttpClientHandler clientHandler)
                                // always verify the SSL certificate
                                clientHandler.ServerCertificateCustomValidationCallback +=
                                    (sender, certificate, chain, sslPolicyErrors) => { return true; };
                            return message;
                        };
                    })
                    .WithAutomaticReconnect()
                    .Build();

                Console.WriteLine($"Start hub connection {hubConnUrl}");
                await conn.StartAsync();

                var venueId = clientIni.Read("VenueId");
                var hucConnOnUrl = $"{venueId}\\SaveOrder";
                Console.WriteLine($"Config hub on request {hucConnOnUrl}");
                conn.On<OrderDto>(hucConnOnUrl, order =>
                {
                    Console.WriteLine($"Successful test.");
                });

                Console.WriteLine($"Success.");
            }
            catch (Exception ex)
            {
                var a = "---------------------------------";
                Console.WriteLine(a);
                Console.WriteLine(ex.Message);
                Console.WriteLine(a);
                Console.WriteLine(ex.InnerException);
                Console.WriteLine(a);
                Console.WriteLine(ex.Source);
                Console.WriteLine(a);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(a);
                Console.WriteLine(ex.Data);
                Console.WriteLine(a);
                Console.WriteLine(ex.HelpLink);
            }

            Console.ReadKey();
        }
    }
}
