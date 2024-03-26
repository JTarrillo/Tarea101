using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServidorWebSocket
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Cargar el contenido del JSON desde el archivo
            string jsonFilePath = "config.json";
            string jsonContent = File.ReadAllText(jsonFilePath);
            Console.WriteLine("Contenido del JSON cargado correctamente");
            
            //Configurar el servidor WebSocket
            HttpListener httpListener = new HttpListener();

            httpListener.Prefixes.Add("http://localhost:8000/");
            httpListener.Start();
            Console.WriteLine("Servidor WebSocket en ejecución...");


            try
            {

                //Blucle principal para manejar las solicitudes entrantes continuamente
                while (true)
                {
                    //Esperar una solicitud HTTP entrante
                    HttpListenerContext contextoDeSolicitud = await httpListener.GetContextAsync();

                    if (contextoDeSolicitud.Request.IsWebSocketRequest)
                    {

                        //Aceptar la solicitud WebSocket
                        HttpListenerWebSocketContext webSocketContext = await contextoDeSolicitud.AcceptWebSocketAsync(null);
                        WebSocket webSocket = webSocketContext.WebSocket;
                        Console.WriteLine("Conexión WebSocket establecida.");


                        //Esperar a recibir y enviar datos 

                        await ManejarConexionWebSocket(webSocket, jsonContent);


                    }

                    else
                    {
                        //Responder con un codigo de estado 400
                        contextoDeSolicitud.Response.StatusCode = 400;
                        contextoDeSolicitud.Response.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo una excepción: {ex.Message}");
             
            }
            finally
            {

                //Cerrar el servidor Httplistener para no consumir memoria
                httpListener.Close();
            }
           
        }

        static async Task ManejarConexionWebSocket(WebSocket webSocket,string jsonContent)
        {
            try
            {
                byte[] buffer = new byte[1025];
                while (webSocket.State == WebSocketState.Open)
                {
                    //Esperar a recibir datos del cliente
                    WebSocketReceiveResult result = await  webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                        if (result.MessageType == WebSocketMessageType.Text) {

                        //Procesar los datos recibidos 

                        string mensajeRecibido = Encoding.UTF8.GetString(buffer, 0, result.Count);
                            Console.WriteLine($"Mensaje recibido del cliente:{mensajeRecibido}");

                        //Enviar el contenido del json al cliente

                        byte[] responseBytes = Encoding.UTF8.GetBytes(jsonContent);
                        await webSocket.SendAsync(new ArraySegment<byte>(responseBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                        Console.WriteLine("Contenido del JSON enviado al cliente.");


                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo una excepción: {ex.Message}");
                throw;
            }
            finally
            {
                // Cerrar la conexión WebSocket
                if (webSocket.State == WebSocketState.Open)
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Cerrando conexión", CancellationToken.None);
            }
        }
    }
}
