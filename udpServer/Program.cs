using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial {
    class Program {
        public static void Main (string[] args) {
            var server = new UDPServer();
            server.Initialize();
            server.StartMessageLoop();
            Console.WriteLine("Server listening!");

            Console.ReadLine();
        }
    }
}