using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial {
    class Program {
        public static async Task Main (string[] args) {
            var client = new UDPClient();
            client.Initialize(IPAddress.Loopback, UDPServer.PORT);
            client.StartMessageLoop();
            await client.Send(Encoding.UTF8.GetBytes("Hello!"));
            Console.WriteLine("Message sent!");
            Console.ReadLine();
        }
    }
}