using MQTTnet;
using System.Text;

namespace phonelinkpar2;

public partial class MainPage : ContentPage
{
    private IMqttClient _mqttClient;
    private MqttClientFactory _mqttFactory;

    public MainPage()
    {
        InitializeComponent();
        _mqttFactory = new MqttClientFactory();
        _mqttClient = _mqttFactory.CreateMqttClient();
    }

    public async void OnConectarClicked(object sender, EventArgs e)
    {
        try
        {
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("broker.hivemq.com", 1883)
                .WithClientId("Cliente_MAUI_App")
                .Build();

            _mqttClient.ApplicationMessageReceivedAsync += evt =>
            {
                string mensaje = evt.ApplicationMessage.ConvertPayloadToString();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    TxtChat.Text += $"[PC dice]: {mensaje}\n";
                });

                return Task.CompletedTask;
            };

            await _mqttClient.ConnectAsync(options);

            var subscribeOptions = _mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f => f.WithTopic("dispositivo/mensajes"))
                .Build();

            await _mqttClient.SubscribeAsync(subscribeOptions);

            BtnConectar.Text = "Conectado Exitosamente";
            BtnConectar.IsEnabled = false;
            await DisplayAlert("Éxito", "Conectado al servidor MQTT", "OK");
        }
        catch (System.Exception ex)
        {
            await DisplayAlert("Error de Conexión", ex.Message, "OK");
        }
    }

    public async void OnEnviarClicked(object sender, EventArgs e)
    {
        if (_mqttClient == null || !_mqttClient.IsConnected)
        {
            await DisplayAlert("Advertencia", "Primero debes hacer clic en Conectar al Broker.", "OK");
            return;
        }

        string texto = TxtMensaje.Text;

        if (!string.IsNullOrWhiteSpace(texto))
        {
            var message = new MqttApplicationMessageBuilder()
                .WithTopic("pc/mensajes")
                .WithPayload(texto)
                .Build();

            await _mqttClient.PublishAsync(message);

            TxtChat.Text += $"[Tú]: {texto}\n";
            TxtMensaje.Text = string.Empty;
        }
    }
}