using System;
using System.Collections.Generic;
using System.IO;
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
            try
            {
                //Create a client that connects to the specified IP Address and port
                TcpClient client = new TcpClient("127.0.0.1", 8001);

                //Create a stream reader and a stream writer
                StreamReader reader = new StreamReader(client.GetStream());
                StreamWriter writer = new StreamWriter(client.GetStream());

                //Set a string to empty in which the user input will be stored
                string str = string.Empty;
                Console.WriteLine("Start a converstaion...");

                //Create a while loop that loops thorugh as long as str != exit
                while (str != "Exit")
                {
                    
                    //Assign the user input varible to Console.Readline()
                    str = Console.ReadLine();
                    Console.WriteLine();
                    //Här skriver den ut strängen till strömmen som är mellan klienten och servern
                    writer.WriteLine(str);
                    //Återställ strömmen med hjälp av Flush()
                    writer.Flush();

                    //Här läser klienten av det som skickas från servern
                    string server_string = reader.ReadLine();

                    //Skriv ut det som hämtas från servern
                    Console.WriteLine(server_string);

                }
                reader.Close();
                writer.Close();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        } 
    }
}
