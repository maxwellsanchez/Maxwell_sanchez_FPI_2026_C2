using MQTTnet;
using System.Text;

class Program
{
    static async Task Main(string[] args)
    {
        // En MQTTnet v5 se usa directamente MqttClientFactory
        var mqttFactory = new MqttClientFactory();
        using var mqttClient = mqttFactory.CreateMqttClient();

        var mqttClientOptions = new MqttClientOptionsBuilder()
            .WithTcpServer("broker.hivemq.com", 1883)
            .WithClientId("Cliente_PC_Console")
            .Build();

        // Evento al recibir un mensaje del celular
        mqttClient.ApplicationMessageReceivedAsync += e =>
        {
            //  Código correcto para MQTTnet v5:
            string mensaje = e.ApplicationMessage.ConvertPayloadToString();
            Console.WriteLine($"\n[Celular dice]: {mensaje}");
            Console.Write("Escribe un mensaje para el celular: ");
            return Task.CompletedTask;
        };

        // Conectar al servidor
        await mqttClient.ConnectAsync(mqttClientOptions);
        Console.WriteLine("Conectado al Broker MQTT exitosamente.");

        // Suscribirse al tópico donde publica el celular
        var subscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
            .WithTopicFilter(f => f.WithTopic("pc/mensajes"))
            .Build();

        await mqttClient.SubscribeAsync(subscribeOptions);
        Console.WriteLine("Suscrito al tópico: pc/mensajes\n");

        // Bucle para enviar mensajes al celular
        while (true)
        {
            Console.Write("Escribe un mensaje para el celular: ");
            string? texto = Console.ReadLine();

            if (!string.IsNullOrEmpty(texto))
            {
                var message = new MqttApplicationMessageBuilder()
                    .WithTopic("dispositivo/mensajes")
                    .WithPayload(texto)
                    .Build();

                await mqttClient.PublishAsync(message);
            }
        }
    }
}