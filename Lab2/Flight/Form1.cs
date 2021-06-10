using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flight
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const double dt = 0.01;
        const double g = 9.81;

        double a;
        double v0;
        double y0;

        double t;
        double x;
        double y;

        bool pause = false;
        private void btStart_Click(object sender, EventArgs e)
        {
            a = (double)edAngle.Value;
            v0 = (double)edSpeed.Value;
            y0 = (double)edHeight.Value;

            t = 0;
            x = 0;
            y = y0;

            chart1.ChartAreas[0].AxisX.Maximum = ((v0 * v0) / (2 * g)) * Math.Sin(2 * a * Math.PI / 180) * (1 + Math.Sqrt(1 + (2 * g * y0) / (v0 * v0 * Math.Sin(a * Math.PI / 180) * Math.Sin(a * Math.PI / 180))))+0.3;
            chart1.ChartAreas[0].AxisY.Maximum = y0 + (v0 * v0 * Math.Sin(a * Math.PI / 180) * Math.Sin(a * Math.PI / 180)) / (2 * g)+0.3;

            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(x, y);

            pause = false;
            button1.BackColor = System.Drawing.Color.White;

            timer1.Start();
            button1.Text = "Пауза";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            t += dt;
            double buf = Math.Round(t, 2);
            label5.Text = buf.ToString() + " с";
            x = v0 * Math.Cos(a * Math.PI / 180) * t;
            y = y0 + v0 * Math.Sin(a * Math.PI / 180) * t - g * t * t / 2;
            chart1.Series[0].Points.AddXY(x, y);
            if (y <= 0) timer1.Stop();
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            if (pause == false)
            {
                button1.Text = "Продолжить";
                timer1.Stop();
                button1.BackColor = System.Drawing.Color.Gray;
                pause = true;
            }
            else 
            {
                button1.Text = "Пауза";
                timer1.Start();
                button1.BackColor = System.Drawing.Color.White;
                pause = false;
            }

        }
    }
}
