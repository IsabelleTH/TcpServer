using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpClientSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create a client that connects to the specified IP Address and port
            var client = new TcpClient("127.0.0.1", 8001);
            var stream = client.GetStream();

            Task.Run(() => ListenForMessages(stream));

            while (true)
            {
                var message = Console.ReadLine();

                // Skicka meddelandets storlek (i byte array)
                var bytes = BitConverter.GetBytes(message.Length);
                stream.Write(bytes, 0, bytes.Length);

                // Gör meddelandet till bytearray och skicka
                var messageBytes = Encoding.UTF8.GetBytes(message);
                stream.Write(messageBytes, 0, messageBytes.Length);
            }
        }

        static void ListenForMessages(NetworkStream stream)
        {
            while (true)
            {
                // Lyssna på hur stort inkommande paket är
                var sizeBuffer = new byte[10];
                stream.Read(sizeBuffer, 0, sizeBuffer.Length);

                var packageSize = BitConverter.ToInt32(sizeBuffer, 0);

                // Skapa en array med den storleken
                var messageBuffer = new byte[packageSize];

                // Läs den storleken
                stream.Read(messageBuffer, 0, messageBuffer.Length);

                // Convertera bytearray till string
                var message = Encoding.UTF8.GetString(messageBuffer);
                Console.WriteLine(message);
            }
        }
    }
}
