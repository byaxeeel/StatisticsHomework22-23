using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Windows.Forms;

namespace HW8._1
{
    public partial class Form1 : Form
    {
        Random r = new Random();
        Pen PenTrajectoryB = new Pen(Color.Black);
        Bitmap b;
        Graphics g;


        Bitmap bHistogramX;
        Graphics gHistogramX;

        Bitmap bHistogramY;
        Graphics gHistogramY;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.b = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            this.g = Graphics.FromImage(b);
            this.g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.g.Clear(Color.White);

            Rectangle VirtualWindow = new Rectangle(0, 0, this.b.Width - 1, this.b.Height - 1);
            g.DrawRectangle(Pens.Black, VirtualWindow);

            int nPoints = (int)numericUpDown1.Value;
            int rayDefault = (int)numericUpDown2.Value;

            double minX = -100;
            double maxX = 100;
            double minY = -100;
            double maxY = 100;

            //genero le coordinate. 
            //raggio r ed angolo a

            List<double> PuntiX = new List<double>();
            List<double> PuntiY = new List<double>();


            for (int i = 0; i < nPoints; i++)
            {
                double ray = r.NextDouble() * rayDefault;
                double angle = r.Next(0,360);
                double xCoord = ray * Math.Cos(angle);
                double yCoord = ray * Math.Sin(angle);

                int xDevice = FromXRealToXVirtual(xCoord, minX, maxX, VirtualWindow.Left, VirtualWindow.Width);
                int yDevice = FromYRealToYVirtual(yCoord, minY, maxY, VirtualWindow.Top, VirtualWindow.Height);

                Rectangle rect = new Rectangle(xDevice, yDevice, 1, 1);
                g.DrawRectangle(PenTrajectoryB, rect);
                g.FillRectangle(Brushes.Black, rect);

                PuntiX.Add(xDevice);
                PuntiY.Add(yDevice);

            }

            this.pictureBox1.Image = b;

            //now let's create Histograms.
            //firstly X's one.
            this.bHistogramX = new Bitmap(this.pictureBox2.Width, this.pictureBox2.Height);
            this.gHistogramX = Graphics.FromImage(bHistogramX);
            this.gHistogramX.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.gHistogramX.Clear(Color.White);

            Rectangle VirtualWindow2 = new Rectangle(0, 0, this.bHistogramX.Width - 1, this.bHistogramX.Height - 1);
            gHistogramX.DrawRectangle(Pens.Black, VirtualWindow2);

            double minValueX = PuntiX.Min();
            double maxValueX = PuntiX.Max();
            double delta = maxValueX - minValueX;
            double nintervals = 15;
            double intervalsSize = delta / nintervals;

            Dictionary<double, int> istogramDict = new Dictionary<double, int>();

            double tempValue = minValueX;
            for (int i = 0; i < nintervals; i++)
            {
                istogramDict[tempValue] = 0;
                tempValue = tempValue + intervalsSize;
            }

            int total = 0;

            foreach (double value in PuntiX)
            {
                foreach (double key in istogramDict.Keys)
                {
                    if (value < key + intervalsSize)
                    {
                        istogramDict[key] += 1;
                        if (total < istogramDict[key])
                        {
                            total = istogramDict[key];
                        }
                        break;
                    }
                }
            }

            List<Control> labelList = new List<Control>();

            foreach (Control ctrl in this.Controls.OfType<Label>().Where(x => x.Name.Contains("tempLabel")))
            {
                labelList.Add(ctrl);
            }

            foreach (Control ctrl in labelList)
            {
                this.Controls.Remove(ctrl);
            }


            gHistogramX.TranslateTransform(0, this.bHistogramX.Height);
            gHistogramX.ScaleTransform(1, -1);

            int idIstogram = 0;
            int widthIstogram = (int)(this.bHistogramX.Width / nintervals);
            double lastKey = 0;

            foreach (double key in istogramDict.Keys)
            {
                lastKey = key;
                int newHeight = istogramDict[key] * this.bHistogramX.Height / total;
                int newX = (widthIstogram * idIstogram) + 1;
                Rectangle isto = new Rectangle(newX, 0, widthIstogram, newHeight);
                idIstogram++;

                Label label = new Label();
                label.Name = "tempLabel";
                if (key <100) label.Location = new Point(newX + this.pictureBox2.Location.X - 5, this.pictureBox2.Height + this.pictureBox2.Location.Y);
                else label.Location = new Point(newX + this.pictureBox2.Location.X - 10, this.pictureBox2.Height + this.pictureBox2.Location.Y);

                label.Text = ((int)(key)).ToString();
                label.Visible = true;
                label.AutoSize = true;
                label.Font = new Font("Calibri", 6.5F);
                label.ForeColor = Color.Black;
                this.Controls.Add(label);

                gHistogramX.DrawRectangle(Pens.Black, isto);
                gHistogramX.FillRectangle(Brushes.Orange, isto);
            }

            Label label2 = new Label();
            label2.Name = "tempLabel";
            label2.Location = new Point(this.pictureBox2.Width + this.pictureBox2.Location.X - 10, this.pictureBox2.Height + this.pictureBox2.Location.Y);
            label2.Text = ((int)(lastKey + intervalsSize)).ToString();
            label2.Visible = true;
            label2.AutoSize = true;
            label2.Font = new Font("Calibri", 6.5F);
            label2.ForeColor = Color.Black;
            this.Controls.Add(label2);

            this.pictureBox2.Image = bHistogramX;

            //Y's one.

            this.bHistogramY = new Bitmap(this.pictureBox3.Width, this.pictureBox3.Height);
            this.gHistogramY = Graphics.FromImage(bHistogramY);
            this.gHistogramY.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.gHistogramY.Clear(Color.White);

            Rectangle VirtualWindow3 = new Rectangle(0, 0, this.bHistogramY.Width - 1, this.bHistogramY.Height - 1);
            gHistogramY.DrawRectangle(Pens.Black, VirtualWindow3);

            double minValueY = PuntiY.Min();
            double maxValueY = PuntiY.Max();
            double deltaY = maxValueY - minValueY;
            double intervalsYSize = deltaY / nintervals;

            Dictionary<double, int> istogramDictY = new Dictionary<double, int>();

            double tempValueY = minValueY;
            for (int i = 0; i < nintervals; i++)
            {
                istogramDictY[tempValueY] = 0;
                tempValueY = tempValueY + intervalsYSize;
            }

            int totalY = 0;

            foreach (double value in PuntiY)
            {
                foreach (double key in istogramDictY.Keys)
                {
                    if (value < key + intervalsYSize)
                    {
                        istogramDictY[key] += 1;
                        if (totalY < istogramDictY[key])
                        {
                            totalY = istogramDictY[key];
                        }
                        break;
                    }
                }
            }

            gHistogramY.TranslateTransform(0, this.bHistogramY.Height);
            gHistogramY.ScaleTransform(1, -1);

            idIstogram = 0;
            int widthIstogramY = (int)(this.bHistogramY.Width / nintervals);
            double lastKeyY = 0;

            foreach (double key in istogramDictY.Keys)
            {
                lastKeyY = key;
                int newHeight = istogramDictY[key] * this.bHistogramY.Height / totalY;
                int newX = (widthIstogramY * idIstogram) + 1;
                Rectangle isto = new Rectangle(newX, 0, widthIstogramY, newHeight);
                idIstogram++;

                Label label = new Label();
                label.Name = "tempLabel";
                if (key < 100) label.Location = new Point(newX + this.pictureBox3.Location.X - 5, this.pictureBox3.Height + this.pictureBox3.Location.Y);
                else label.Location = new Point(newX + this.pictureBox3.Location.X - 10, this.pictureBox3.Height + this.pictureBox3.Location.Y);

                label.Text = ((int)(key)).ToString();
                label.Visible = true;
                label.AutoSize = true;
                label.Font = new Font("Calibri", 6.5F);
                label.ForeColor = Color.Black;
                this.Controls.Add(label);

                gHistogramY.DrawRectangle(Pens.Black, isto);
                gHistogramY.FillRectangle(Brushes.Orange, isto);


            }

            Label label3 = new Label();
            label3.Name = "tempLabel";
            label3.Location = new Point(this.pictureBox3.Width + this.pictureBox3.Location.X - 10, this.pictureBox3.Height + this.pictureBox3.Location.Y);
            label3.Text = ((int)(lastKeyY + intervalsYSize)).ToString();
            label3.Visible = true;
            label3.AutoSize = true;
            label3.Font = new Font("Calibri", 6.5F);
            label3.ForeColor = Color.Black;
            this.Controls.Add(label3);

            this.pictureBox3.Image = bHistogramY;

        }

        private int FromXRealToXVirtual(double X, double minX, double maxX, int Left, int W)
        {
            if (maxX - minX == 0)
            {
                return 0;
            }
            else
            {
                return (int)(Left + W * (X - minX) / (maxX - minX));
            }
        }

        private int FromYRealToYVirtual(double Y, double minY, double maxY, int Top, int H)
        {
            if (maxY - minY == 0)
            {
                return 0;
            }
            else
            {
                return (int)(Top + H - H * (Y - minY) / (maxY - minY));
            }
        }
    }
}