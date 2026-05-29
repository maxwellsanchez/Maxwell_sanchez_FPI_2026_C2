using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculadora
{
    public partial class Form1 : Form
    {
        private double valor1;
        private double valor2;

        private double resultado1;

        public int operacion;


        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            //numero 0
            resultado.Text = resultado.Text + "0";
        }

        private void punto_Click(object sender, EventArgs e)
        {
            //punto
            resultado.Text = resultado.Text + ".";
        }

        private void igual_Click(object sender, EventArgs e)
        {
            //igual
            valor2 = Convert.ToDouble(resultado.Text);
            switch (operacion)
            {
                case 1:
                    resultado1 = valor1 + valor2;
                    break;
                          case 2:
                                resultado1 = valor1 - valor2;
                                break;
                                      case 3:
                                             resultado1 = valor1 * valor2;
                                             break;
                                                  case 4:
                                                         resultado1 = valor1 / valor2;
                                                         break;

            }

            
            resultado.Text = resultado1.ToString(); 
        }

        private void btn_1_Click(object sender, EventArgs e)
        {
            //numero 1
            resultado.Text = resultado.Text + "1";
        }

        private void btn_2_Click(object sender, EventArgs e)
        {
            //numero 2
            resultado.Text = resultado.Text + "2";
        }

        private void btn_3_Click(object sender, EventArgs e)
        {
            //numero 3
            resultado.Text = resultado.Text + "3";
        }

        private void btn_4_Click(object sender, EventArgs e)
        {
            //numero 4
            resultado.Text = resultado.Text + "4";
        }

        private void btn_5_Click(object sender, EventArgs e)
        {
            //numero 5
            resultado.Text = resultado.Text + "5";
        }

        private void btn_6_Click(object sender, EventArgs e)
        {
            //numero 6
            resultado.Text = resultado.Text + "6";
        }

        private void btn_7_Click(object sender, EventArgs e)
        {
            //numero 7
            resultado.Text = resultado.Text + "7";
        }

        private void btn_8_Click(object sender, EventArgs e)
        {
            //numero 8
            resultado.Text = resultado.Text + "8";
        }

        private void btn_9_Click(object sender, EventArgs e)
        {
            //numero 9
            resultado.Text = resultado.Text + "9";
        }

        private void suma_Click(object sender, EventArgs e)
        {
            //suma
            operacion = 1;
            valor1 = Convert.ToDouble(resultado.Text);
            resultado.Text = "";
        }

        private void resta_Click(object sender, EventArgs e)
        {
            //resta
            operacion = 2;
            valor1 = Convert.ToDouble(resultado.Text);
            resultado.Text = "";
        }

        private void multi_Click(object sender, EventArgs e)
        {
            //multiplicacion
            operacion = 3;
            valor1 = Convert.ToDouble(resultado.Text);
            resultado.Text = "";
        }

        private void div_Click(object sender, EventArgs e)
        {
            //division
            operacion = 4;
            valor1 = Convert.ToDouble(resultado.Text);
            resultado.Text = "";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void clear_Click(object sender, EventArgs e)
        {
            //clear
            resultado.Text = "";
        }

        private void resultado_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
