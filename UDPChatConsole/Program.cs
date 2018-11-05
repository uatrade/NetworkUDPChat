using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UDPChatConsole
{
    class Program
    {
        static string remoteAddress; // хост для отправки данных
        static int remotePort; // порт для отправки данных
        static int localPort; // локальный порт для входящих подключений
        private static void SendMessage()
        {
            UdpClient sender = new UdpClient();

            try
            {
                while (true)
                {
                    Console.WriteLine("Введите сообщение");
                    string message = Console.ReadLine();
                    byte[] data = Encoding.UTF8.GetBytes(message);

                    sender.Send(data, data.Length, remoteAddress, remotePort);
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                sender.Close();
            }
        }

        private static void ReceiveMessage()
        {
            UdpClient receiver = new UdpClient(localPort);
            IPEndPoint iPEndPoint = null;

            try
            {
                while (true)
                {

                byte[] bytes=receiver.Receive(ref iPEndPoint);

                string message = Encoding.UTF8.GetString(bytes);

                Console.WriteLine($"Входящее сообщение: {message} ");
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                receiver.Close();
            }

        }

        static void Main(string[] args)
        {
           

            try
            {
                Console.Write("Введите порт для прослушивания: "); // локальный порт
                localPort = Int32.Parse(Console.ReadLine());
                Console.Write("Введите удаленный адрес для подключения: ");
                remoteAddress = Console.ReadLine(); // адрес, к которому мы подключаемся
                Console.Write("Введите порт для подключения: ");
                remotePort = Int32.Parse(Console.ReadLine()); // порт, к которому мы подключаемся

                Task sendTask = new Task(SendMessage);
                sendTask.Start();


                ReceiveMessage();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
