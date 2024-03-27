# Implementación de WebSockets en C# (.NET Framework)

## Objetivo

Decidí implementar la comunicación bidireccional entre dos aplicaciones web utilizando WebSockets en C# utilizando .NET Framework. Aquí está mi proceso paso a paso:

## Pasos que Seguí

### Crear un Proyecto de Consola (.NET Framework)

Abrí Visual Studio y creé un nuevo proyecto de consola utilizando .NET Framework. Opté por esta opción ya que es compatible con las versiones anteriores de .NET.

### Instalar los Paquetes NuGet

Utilicé el administrador de paquetes NuGet para instalar los paquetes necesarios para trabajar con WebSockets en .NET Framework. Esto incluyó la instalación del paquete `WebSocketSharp` para la implementación del servidor WebSocket y `Newtonsoft.Json` para la serialización y deserialización de mensajes JSON.

### Configurar el Servidor WebSocket

En el archivo `Program.cs`, creé un método para configurar y ejecutar el servidor WebSocket. Aquí establecí el punto de entrada y la lógica para manejar las conexiones WebSocket entrantes.

### Manejar las Conexiones WebSocket

Implementé la lógica para manejar las conexiones WebSocket entrantes. Esto incluyó la aceptación de conexiones, la recepción y el envío de mensajes, así como el cierre de las conexiones cuando fuera necesario.

### Enviar y Recibir Mensajes JSON

Utilicé la biblioteca Newtonsoft.Json para serializar y deserializar mensajes en formato JSON que se enviaron y recibieron a través de las conexiones WebSocket.

### Ejecutar el Servidor WebSocket

En el método `Main`, llamé al método de configuración del servidor WebSocket y ejecuté la aplicación.

### Configurar el Cliente WebSocket

Creé otro proyecto de consola utilizando .NET Framework para el cliente WebSocket. Instalé los paquetes necesarios y configuré el cliente WebSocket para conectarse al servidor.

### Establecer la Conexión WebSocket

Utilicé la biblioteca `WebSocketSharp` para establecer una conexión WebSocket con el servidor. Esto me permitió enviar y recibir mensajes a través de la conexión.


## Puntos Adicionales

**1 - Pruebas exhaustivas para garantizar la transferencia correcta de datos en ambas direcciones (ya está incluido en la solución).**

**2 - Optimización del rendimiento de la transferencia de datos (ya está incluido en la solución).**


## Pruebas y Depuración

Realicé pruebas exhaustivas para garantizar que la comunicación funcionara correctamente en ambas direcciones. Utilicé herramientas de depuración para detectar y corregir cualquier problema que encontré durante el desarrollo.

