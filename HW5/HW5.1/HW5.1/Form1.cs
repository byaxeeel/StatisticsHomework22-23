using System.Diagnostics.Metrics;
using System.Windows.Forms;

namespace HW5._1
{
    public partial class Form1 : Form
    {
        Bitmap Histogram;
        Graphics g;
        Random r = new Random();
        Pen PenTrajectoryOrange = new Pen(Color.Orange, 2);
       
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //VERTICAL HISTOGRAMS
            this.richTextBox1.Text = "";

            this.Histogram = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            this.g = Graphics.FromImage(Histogram);
            this.g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.g.Clear(Color.White);

            double numberOfBalls = Convert.ToDouble(numericUpDown1.Value);
            double SuccessProbability = (1 / numberOfBalls);
            int Trials = (int)numericUpDown2.Value;

            Rectangle VirtualWindow = new Rectangle(0, 0, this.Histogram.Width - 1, this.Histogram.Height - 1);
            g.DrawRectangle(Pens.Black, VirtualWindow);

            Dictionary<int, int> nBall_nWins = new Dictionary<int, int>();
            for (int j = 1; j <= numberOfBalls; j++)
            {
                nBall_nWins[j] = 0;
            }

            for (int i = 0; i < Trials; i++)
            {
                
                double randomValue = r.NextDouble();
                while (randomValue == 0.0)
                {
                    randomValue = r.NextDouble();
                }
                int winnerBall = (int)(((randomValue / SuccessProbability) - 0.01) +1);
                if (winnerBall == 0) winnerBall = 1;
                nBall_nWins[winnerBall]++;
            }

            //proporzionare
            int total = 1;

            foreach (int key in nBall_nWins.Keys)
            {
                total += nBall_nWins[key];
            }

            g.TranslateTransform(0, this.Histogram.Height);
            g.ScaleTransform(1, -1);

            int numberOfBalls2 = 0;

            int nlabel = 0;
            Boolean fine = false;

            List<Control> labelList = new List<Control>();

            //remove old label cause number of balls can change
            foreach (Control ctrl in this.Controls.OfType<Label>().Where(x => x.Name.Contains("tempLabel")))
            {
                labelList.Add(ctrl);
            }

            foreach (Control ctrl in labelList)
            {
                this.Controls.Remove(ctrl);
            }
            
          
            foreach (int key in nBall_nWins.Keys)
            {
                this.richTextBox1.Text = this.richTextBox1.Text + "Ball n"+ (numberOfBalls2 + 1).ToString() + " wins:" + nBall_nWins[key].ToString() + "\n";
                int newHeight = nBall_nWins[key] * this.Histogram.Height / total;
                int newX = (int)((this.Histogram.Width / numberOfBalls) * numberOfBalls2);
                Rectangle VirtualWindow1 = new Rectangle(newX, 0, (int) (this.Histogram.Width / numberOfBalls), newHeight);

                int nextX = (int)((this.Histogram.Width / numberOfBalls) * (numberOfBalls2 + 1));

                Label label = new Label();
                label.Name = "tempLabel";
                if (numberOfBalls2 < 10)
                {
                    label.Location = new Point(newX + this.pictureBox1.Location.X + ((nextX - newX) / 2) - 5, this.pictureBox1.Height + this.pictureBox1.Location.Y);
                }
                else
                {
                    label.Location = new Point(newX + this.pictureBox1.Location.X + ((nextX - newX) / 2) - 10, this.pictureBox1.Height + this.pictureBox1.Location.Y);

                }
                label.Text = (numberOfBalls2+1).ToString();
                label.Visible = true;
                label.AutoSize = true;
                label.Font = new Font("Calibri", 10);
                label.ForeColor = Color.Green;

                this.Controls.Add(label);

                numberOfBalls2++;
                g.DrawRectangle(Pens.Black, VirtualWindow1);
                //g.FillRectangle(Brushes.Orange, VirtualWindow1);
            }

            this.pictureBox1.Image = Histogram;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //HORIZONTAL HISTOGRAMS
            this.richTextBox1.Text = "";

            this.Histogram = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            this.g = Graphics.FromImage(Histogram);
            this.g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.g.Clear(Color.White);

            double numberOfBalls = Convert.ToDouble(numericUpDown1.Value);
            double SuccessProbability = (1 / numberOfBalls);
            int Trials = (int)numericUpDown2.Value;

            Rectangle VirtualWindow = new Rectangle(0, 0, this.Histogram.Width - 1, this.Histogram.Height - 1);
            g.DrawRectangle(Pens.Black, VirtualWindow);

            Dictionary<int, int> nBall_nWins = new Dictionary<int, int>();
            for (int j = 1; j <= numberOfBalls; j++)
            {
                nBall_nWins[j] = 0;
            }

            for (int i = 0; i < Trials; i++)
            {

                double randomValue = r.NextDouble();
                while (randomValue == 0.0)
                {
                    randomValue = r.NextDouble();
                }
                int winnerBall = (int)(((randomValue / SuccessProbability) - 0.01) + 1);
                if (winnerBall == 0) winnerBall = 1;
                nBall_nWins[winnerBall]++;
            }

            //proporzionare
            int total = 1;

            foreach (int key in nBall_nWins.Keys)
            {
                total += nBall_nWins[key];
            }

            int numberOfBalls2 = 0;

            int nlabel = 0;
            Boolean fine = false;

            List<Control> labelList = new List<Control>();

            //remove old label cause number of balls can change
            foreach (Control ctrl in this.Controls.OfType<Label>().Where(x => x.Name.Contains("tempLabel")))
            {
                labelList.Add(ctrl);
            }

            foreach (Control ctrl in labelList)
            {
                this.Controls.Remove(ctrl);
            }


            foreach (int key in nBall_nWins.Keys)
            {
                this.richTextBox1.Text = this.richTextBox1.Text + "Ball n" + (numberOfBalls2 + 1).ToString() + " wins:" + nBall_nWins[key].ToString() + "\n";
                int newWidth = nBall_nWins[key] * this.Histogram.Width / total;
                int newY = (int)((this.Histogram.Height / numberOfBalls) * numberOfBalls2);
                Rectangle VirtualWindow1 = new Rectangle(0, newY, newWidth,(int)(this.Histogram.Height / numberOfBalls));

                int nextY = (int)((this.Histogram.Height / numberOfBalls) * (numberOfBalls2 + 1));

                Label label = new Label();
                label.Name = "tempLabel";
                if (numberOfBalls2 < 10)
                {
                    label.Location = new Point(this.pictureBox1.Location.X-20, newY + this.pictureBox1.Location.Y + ((nextY - newY) / 2) - 5);
                }
                else
                {
                    label.Location = new Point(this.pictureBox1.Location.X-25, newY + this.pictureBox1.Location.Y + ((nextY - newY) / 2) - 5);

                }
                label.Text = (numberOfBalls2 + 1).ToString();
                label.Visible = true;
                label.AutoSize = true;
                label.Font = new Font("Calibri", 10);
                label.ForeColor = Color.Green;

                this.Controls.Add(label);

                numberOfBalls2++;
                g.DrawRectangle(Pens.Black, VirtualWindow1);
                //g.FillRectangle(Brushes.Orange, VirtualWindow1);
            }

            this.pictureBox1.Image = Histogram;

        }
    }
}