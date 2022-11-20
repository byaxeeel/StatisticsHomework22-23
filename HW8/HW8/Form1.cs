using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace HW8
{
    public partial class Form1 : Form
    {
        Random r = new Random();
        Pen PenTrajectoryG = new Pen(Color.Gray, 0.5F);
        Bitmap bHistogram;
        Graphics gHistogram;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.bHistogram = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            this.gHistogram = Graphics.FromImage(bHistogram);
            this.gHistogram.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.gHistogram.Clear(Color.White);

            Rectangle VirtualWindow = new Rectangle(0, 0, this.bHistogram.Width - 1, this.bHistogram.Height - 1);
            gHistogram.DrawRectangle(Pens.Black, VirtualWindow);

            double minValue = -20;
            double maxValue = 20;

            if (this.radioButton1.Checked) {
                minValue = -4;
                maxValue = 4;
            }
            else if (this.radioButton2.Checked) {
                minValue = 0;
                maxValue = 5;
            }
            else if (this.radioButton3.Checked)
            {
                minValue = -15;
                maxValue = 15;
            }
            else if (this.radioButton4.Checked)
            {
                minValue = 0;
                maxValue = 10;
            }
            else if (this.radioButton5.Checked)
            {
                minValue = -10;
                maxValue = 10;
            }

            double delta = maxValue - minValue;
            double nintervals = 150;
            double intervalsSize = delta / nintervals;

            int nRows = (int)nintervals;
            int nCols = (int)nintervals;

            int nTrials = (int)numericUpDown1.Value;

            Dictionary<double, int> istogramDict = new Dictionary<double, int>();
            double tempValue = minValue;
            for (int i = 0; i < nintervals; i++)
            {
                istogramDict[tempValue] = 0;
                tempValue = tempValue + intervalsSize; 
            }

            int total = 0;

            for (int x = 0; x < nTrials; x++)
            {
                double xRnd = r.NextDouble() * (1 - -1) + -1;
                double value = 0;
                double yRnd = r.NextDouble() * (1 - -1) + -1; 

                double s = (xRnd * xRnd) + (yRnd * yRnd);

                while (s < 0 || s > 1) 
                {
                    xRnd = r.NextDouble();
                    yRnd = r.NextDouble();
                    s = (xRnd * xRnd) + (yRnd * yRnd);
                }

                xRnd = xRnd * Math.Sqrt(-2 * Math.Log2(s) / s);
                yRnd = yRnd * Math.Sqrt(-2 * Math.Log2(s) / s);

                if (this.radioButton1.Checked) value = xRnd;
                else if (this.radioButton2.Checked) value = xRnd * xRnd;
                else if (this.radioButton3.Checked) value = xRnd / (yRnd * yRnd);
                else if (this.radioButton4.Checked) value = (xRnd * xRnd) / (yRnd * yRnd);
                else if (this.radioButton5.Checked) value = xRnd / yRnd;

                foreach (double key in istogramDict.Keys)
                {
                    double range = key + intervalsSize;
                    if (range > maxValue) range = maxValue;
                    if (value < range && value > key)
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

            gHistogram.TranslateTransform(0, this.bHistogram.Height);
            gHistogram.ScaleTransform(1, -1);

            int idIstogram = 0;
            int widthIstogram = (int)(this.bHistogram.Width / nintervals);
            foreach (double key in istogramDict.Keys)
            {
                int newHeight = istogramDict[key] * this.bHistogram.Height / total;
                int newX = (widthIstogram * idIstogram) + 1;
                Rectangle isto = new Rectangle(newX, 0, widthIstogram, newHeight);
                idIstogram++;

                int nextWidthIstogram = (int)(widthIstogram * idIstogram * 1);

                gHistogram.DrawRectangle(Pens.Black, isto);
                gHistogram.FillRectangle(Brushes.Orange, isto);

                if ((idIstogram-1) % 10 != 0 && idIstogram != istogramDict.Keys.Count()) continue;

                Label label = new Label();
                label.Name = "tempLabel";
                if (key < 0) label.Location = new Point(newX + this.pictureBox1.Location.X + 20, this.pictureBox1.Height + this.pictureBox1.Location.Y);             
                else label.Location = new Point(newX + this.pictureBox1.Location.X + 25, this.pictureBox1.Height + this.pictureBox1.Location.Y);

                if ((idIstogram) == istogramDict.Keys.Count()) label.Text = ((double)(key)).ToString("N2") + " : " + ((double)(maxValue)).ToString("N2");
                else label.Text = ((double)(key)).ToString("N2") + " : " + ((double)(key + (intervalsSize*10))).ToString("N2");
                label.Visible = true;
                label.AutoSize = true;
                label.Font = new Font("Calibri", 7);

                label.ForeColor = Color.Black;
                this.Controls.Add(label);
            }

            int inverseI = nRows;
            for (int i = 0; i <= nRows; i++)
            {
                if (i % 10 != 0)
                {
                    inverseI--;
                    continue;
                }

                Point p1 = new Point(0, (int)this.pictureBox1.Height / nRows * i);
                Point p2 = new Point(this.pictureBox1.Width, (int)this.pictureBox1.Height / nRows * i);

                gHistogram.DrawLine(PenTrajectoryG, p1, p2);

                Label label = new Label();
                label.Name = "tempLabel";

                label.Location = new Point(this.pictureBox1.Location.X - 40, (int)this.pictureBox1.Location.Y + (this.pictureBox1.Height / nRows * i) -5);

                label.Text = (total / nintervals * inverseI).ToString("N2");
                inverseI--;
                label.Visible = true;
                label.AutoSize = true;
                label.Font = new Font("Calibri", 7);

                label.ForeColor = Color.Black;
                this.Controls.Add(label);
            }

            for (int i = 0; i <= nCols; i++)
            {
                if (i % 10 != 0) continue;

                Point p1 = new Point((int)this.pictureBox1.Width / nCols * i, 0);
                Point p2 = new Point((int)this.pictureBox1.Width / nCols * i, (int)this.pictureBox1.Height);

                gHistogram.DrawLine(PenTrajectoryG, p1, p2);

            }

            this.pictureBox1.Image = bHistogram;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}