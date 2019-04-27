using Newtonsoft.Json;

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace iBank.BankProvider.VTB
{
    public class BankProviderServer
    {
        public Socket TCPListener { get; }

        public BankProviderServer(IPAddress ipAddress, ushort port)
        {
            System.Console.WriteLine($"Сервер запущен, порт {port}.");
            TCPListener = new Socket(SocketType.Stream, ProtocolType.Tcp);
            TCPListener.Bind(new IPEndPoint(ipAddress, port));
        }

        public void Start()
        {
            TCPListener.Listen(0);
            while (true)
            {
                var client = TCPListener.Accept();
                new Thread(() =>
                {
                    try
                    {
                        System.Console.WriteLine($"Клиент подключился: {(IPEndPoint) client.RemoteEndPoint}.");
                        var stream = new NetworkStream(client);
                        using (var reader = new BinaryReader(stream, Encoding.UTF8, true))
                        using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
                        {
                            while (client.Connected)
                            {
                                if (client.Available == 0)
                                    Thread.Sleep(10);
                                else
                                {
                                    var function = reader.ReadByte();
                                    switch (function)
                                    {
                                        case 1: // GetExecutedByDate
                                            var dateTime = new DateTime(reader.ReadInt64());
                                            writer.Write(JsonConvert.SerializeObject(BankProviderPersonExtensions.GetExecutedByDate(dateTime).ToList()));
                                            break;
                                        case 2: // GetAll
                                            writer.Write(JsonConvert.SerializeObject(BankProviderPersonExtensions.GetAll().ToList()));
                                            break;
                                    }
                                    System.Console.WriteLine($"Клиент запросил: {function}.");
                                    writer.Flush();
                                }
                            }
                        }
                        System.Console.WriteLine($"Клиент отключился.");
                    }
                    catch (Exception e) { System.Console.WriteLine(e.ToString()); }

                }).Start();
            }
        }
    }
}