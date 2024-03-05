using System.Net;
using System.Net.Sockets;
using System.Text;
namespace Program
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
            Console.Write("сервер? ");
            bool server = (Console.ReadLine() ?? "").Equals("y");
            int clientPort = 0;
            if (server)
            {
                Console.Write("порт назначения: ");
                clientPort = int.Parse(Console.ReadLine() ?? "");
            }
            int port;
            if (!server)
            {
                Console.Write("порт: ");
                port = int.Parse(Console.ReadLine() ?? "");
            }
            else {port = 9; };
            IPEndPoint endPoint = new IPEndPoint(
            IPAddress.Parse("127.0.0.1"),
            port
            );
            // UDP Сокеты
            Socket socket = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Dgram,
            ProtocolType.Udp
            );
            socket.Bind(endPoint);
            if (server)
            {
                IPEndPoint clientEndPoint = new IPEndPoint(
                IPAddress.Parse("127.0.0.1"),
                clientPort
                );
                byte[] bytes = new byte[5000];
                for (int i = 0; i < 200; i++)
                {
                    bytes[0] = (byte)i;
                    socket.SendTo(bytes, clientEndPoint);
                }
            }
            else
            {
                EndPoint serverEndPoint = new
                IPEndPoint(IPAddress.Any, 0);
                byte[] bytes = new byte[5000];
                while (true)
                {
                    int size = socket.ReceiveFrom(bytes, ref
                    serverEndPoint);
                    Encoding.UTF8.GetString(bytes);
                    Console.WriteLine((int)bytes[0]);
                }
            }
        }
    }
}