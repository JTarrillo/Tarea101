using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Threading;
using Newtonsoft.Json;

namespace ClienteWebSocket
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //URL del servidor WebSocket
            string serverUrl = "ws://localhost:8000/";
            //Crear un cliente WebSocket

            using (ClientWebSocket clienteWebSocket = new ClientWebSocket())
            {
                try
                {
                    //Conectar al servidor WebsOCKET
                    await clienteWebSocket.ConnectAsync(new Uri(serverUrl), CancellationToken.None);
                    Console.WriteLine("Conexión WebSocket establecida.");
                    //Enviar un mensaje al servidor
                    string message = "Hola desde el cliente";
                    byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                    await clienteWebSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);

                    //Esperar y recibir respuesta del servidor

                    byte[] buffer = new byte[1025];

                    while(true) { 
                    
                    WebSocketReceiveResult result = await clienteWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                        if(result.MessageType == WebSocketMessageType.Text)
                        {
                            // Procesar los datos recibidos del servidor (que se espera que sea un JSON)
                            string receivedJson = Encoding.UTF8.GetString(buffer, 0, result.Count);
                            Console.WriteLine($"JSON recibido del servidor: {receivedJson}");

                            // Deserializar el JSON en un objeto de la clase Config
                            Config config = JsonConvert.DeserializeObject<Config>(receivedJson);

                            // Imprimir los datos del objeto Config
                            Console.WriteLine($"SslCertificateAlwaysValid: {config.SslCertificateAlwaysValid}");
                            Console.WriteLine($"ServerIp: {config.ServerIp}");
                            Console.WriteLine($"ServerPort: {config.ServerPort}");
                            Console.WriteLine($"MaxConnections: {config.MaxConnections}");
                            Console.WriteLine($"WelcomeMessage: {config.WelcomeMessage}");
                            Console.WriteLine($"ErrorMessage: {config.ErrorMessage}");

                            break;
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error de WebSocket: {ex.Message}");
                    throw;
                }

                Console.ReadLine();
            }
        }
    }
}
