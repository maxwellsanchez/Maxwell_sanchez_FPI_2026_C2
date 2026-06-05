using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace juego_de_azar
{
    public partial class Form1 : Form
      
    {
        Random random = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int t1 = random.Next(0, 100);
            int t2 = random.Next(0, 100);
            int t3 = random.Next(0, 100);

            if (int.TryParse(textJ.Text, out int numeroUsuario) && double.TryParse(apuesta.Text, out double cantidadApuesta))
            {
                double ganancia = 0;
                bool huboAcierto = true;

                // Resetea el color por defecto al inicio de cada jugada
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
            }
            else
            {
                // Nota vaga: Validación de datos de entrada correctos en ambos campos
                textL.Text = "Error en los datos.";
                textG.Text = "Ingresa números válidos en la jugada y la apuesta.";
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textG_TextChanged(object sender, EventArgs e)
        {

        }

        private void apuesta_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
