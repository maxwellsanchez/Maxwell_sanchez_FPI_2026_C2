using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace juego_de_azar
{
    public partial class Form1 : Form
    {
        // Nota: Inicialización de variables globales y tópicos únicos
        Random random = new Random();
        MqttClient clienteMqtt;
        string broker = "broker.hivemq.com";

        string topicoJugadas = "itla/20260899/loteria/jugadas";
        string topicoResultados = "itla/20260899/loteria/resultados";
        string topicoPremios = "itla/20260899/loteria/premios";

        public Form1()
        {
            InitializeComponent();
            InicializarMqtt();
        }

        // Nota: Conexión con manejo estricto de excepciones para depuración
        private void InicializarMqtt()
        {
            try
            {
                clienteMqtt = new MqttClient(broker);
                clienteMqtt.MqttMsgPublishReceived += AlRecibirMensajeMqtt;
                string clientId = Guid.NewGuid().ToString();
                clienteMqtt.Connect(clientId);

                if (clienteMqtt.IsConnected)
                {
                    clienteMqtt.Subscribe(new string[] { topicoJugadas }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                    // Nota: Alerta de confirmación de enlace exitoso al iniciar
                    MessageBox.Show("¡Conectado exitosamente al broker HiveMQ!", "Conexión MQTT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error crítico al conectar a MQTT: " + ex.Message, "Error de Red", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nota: Captura de mensajes con aislamiento de errores de conversión
        private void AlRecibirMensajeMqtt(object sender, MqttMsgPublishEventArgs e)
        {
            try
            {
                string payload = Encoding.UTF8.GetString(e.Message);

                // Nota: Invocación segura delegada al hilo de la UI
                this.Invoke((MethodInvoker)delegate {
                    ProcesarJugadaRemota(payload);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al recibir el mensaje de red: " + ex.Message, "Error de Tráfico", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Nota: Inyección de datos remotos y disparo controlado del evento click
        private void ProcesarJugadaRemota(string datos)
        {
            try
            {
                string[] partes = datos.Split(',');
                if (partes.Length == 2)
                {
                    textJ.Text = partes[0].Trim();
                    apuesta.Text = partes[1].Trim();

                    button1.PerformClick();
                }
                else
                {
                    MessageBox.Show("El formato recibido no es correcto. Debe ser 'numero,apuesta' (Ej: 25,100). Recibido: " + datos, "Formato Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error procesando los datos en los controles: " + ex.Message, "Error de UI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int t1 = random.Next(0, 100);
            int t2 = random.Next(0, 100);
            int t3 = random.Next(0, 100);

            string numerosResultado = $"{t1},{t2},{t3}";

            // Nota: Intento de publicación de resultados con validación de estado de conexión
            if (clienteMqtt != null && clienteMqtt.IsConnected)
            {
                clienteMqtt.Publish(topicoResultados, Encoding.UTF8.GetBytes(numerosResultado));
            }
            else
            {
                MessageBox.Show("No se pudo publicar el resultado porque el cliente MQTT se desconectó.", "Error de Envío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (int.TryParse(textJ.Text, out int numeroUsuario) && double.TryParse(apuesta.Text, out double cantidadApuesta))
            {
                double ganancia = 0;
                bool huboAcierto = true;

                textJ.BackColor = System.Drawing.Color.White;

                if (numeroUsuario == t1)
                {
                    ganancia = cantidadApuesta * 1000;
                    textL.Text = $"¡Increíble! Acertaste el primer número (Premio Mayor). Los números fueron: {t1}, {t2}, {t3}.";
                    textG.Text = $"¡JACKPOT! Has ganado: ${ganancia}";
                    textJ.BackColor = System.Drawing.Color.Green;
                }
                else if (numeroUsuario == t2)
                {
                    ganancia = cantidadApuesta * 100;
                    textL.Text = $"¡Muy bien! Acertaste el segundo número. Los números fueron: {t1}, {t2}, {t3}.";
                    textG.Text = $"Has ganado: ${ganancia}";
                    textJ.BackColor = System.Drawing.Color.Green;
                }
                else if (numeroUsuario == t3)
                {
                    ganancia = cantidadApuesta * 10;
                    textL.Text = $"¡Bien! Acertaste el tercer número. Los números fueron: {t1}, {t2}, {t3}.";
                    textG.Text = $"Has ganado: ${ganancia}";
                    textJ.BackColor = System.Drawing.Color.Green;
                }
                else
                {
                    huboAcierto = false;
                }

                if (!huboAcierto)
                {
                    textL.Text = $"No hubo coincidencias. Los números ganadores fueron: {t1}, {t2}, {t3}.";
                    textG.Text = "Mejor suerte la próxima";
                }

                if (clienteMqtt != null && clienteMqtt.IsConnected)
                {
                    string mensajePremio = huboAcierto ? $"GANASTE:{ganancia}" : "PERDISTE:0";
                    clienteMqtt.Publish(topicoPremios, Encoding.UTF8.GetBytes(mensajePremio));
                }
            }
            else
            {
                textL.Text = "Error en los datos.";
                textG.Text = "Ingresa números válidos en la jugada y la apuesta.";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void textG_TextChanged(object sender, EventArgs e) { }
        private void apuesta_TextChanged(object sender, EventArgs e) { }
    }
}