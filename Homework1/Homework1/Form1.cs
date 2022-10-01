using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework1
{
    public partial class Form1 : Form
    {
        Boolean pari = true;
        private Random r = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < 10; i++)
            {
                pictureBox1.BackColor = Color.FromArgb(r.Next(0, 255),r.Next(0, 255), r.Next(0, 255));
                System.Threading.Thread.Sleep(1000);
                this.effect1.Text = (10-i).ToString();
                this.Refresh();
            }
            this.effect1.Text = "Effect 1";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (pari == true)
            {
                this.pictureBox1.Location = new System.Drawing.Point(400, 162);
                pari = false;
            }
            else {
                this.pictureBox1.Location = new System.Drawing.Point(39, 162);
                pari = true;
            }
        }

        private void button1_Hover(object sender, EventArgs e)
        {
            this.button1.BackColor = Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.button1.BackColor = Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
            this.button2.BackColor = Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
            this.effect1.BackColor = Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
        }
    }
}
