using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;

namespace Tutorial {
  public class UDPServer {
    public const int PORT = 5;

    private Socket socket;
    private EndPoint ep;

    private byte[] buffer_recv;
    private ArraySegment<byte> buffer_recv_segment;

    public void Initialize () {
      buffer_recv = new byte[4096];
      buffer_recv_segment = new(buffer_recv);

      ep = new IPEndPoint(IPAddress.Any, PORT);

      socket = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
      socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.PacketInformation, true);
      socket.Bind(ep);
    }

    public void StartMessageLoop () {
      _ = Task.Run(async () =>
      {
        SocketReceiveMessageFromResult res;
        while (true) {
          res = await socket.ReceiveMessageFromAsync(
            buffer_recv_segment, SocketFlags.None, ep);
          
          Console.WriteLine($"Received message: \"{Encoding.UTF8.GetString(buffer_recv, 0, res.ReceivedBytes)}\".");
          await SendTo(res.RemoteEndPoint, Encoding.UTF8.GetBytes("Hello back!"));
        }
      }
      );
    }

    public async Task SendTo (EndPoint recipient, byte[] data) {
      var s = new ArraySegment<byte>(data);
      await socket.SendToAsync(s, SocketFlags.None, recipient);
    }
  }

  public class UDPClient {
    public const int PORT = 5;

    private Socket socket;
    private EndPoint ep;

    private byte[] buffer_recv;
    private ArraySegment<byte> buffer_recv_segment;

    public void Initialize (IPAddress address, int port) {
      buffer_recv = new byte[4096];
      buffer_recv_segment = new(buffer_recv);

      ep = new IPEndPoint(address, port);

      socket = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
      socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.PacketInformation, true);
    }

    public void StartMessageLoop () {
      _ = Task.Run(async () =>
      {
        SocketReceiveMessageFromResult res;
        while (true) {
          res = await socket.ReceiveMessageFromAsync(
            buffer_recv_segment, SocketFlags.None, ep);
          
          Console.WriteLine($"Received message: \"{Encoding.UTF8.GetString(buffer_recv, 0, res.ReceivedBytes)}\".");
        }
      }
      );
    }

    public async Task Send (byte[] data) {
      var s = new ArraySegment<byte>(data);
      await socket.SendToAsync(s, SocketFlags.None, ep);
    }
  }

}