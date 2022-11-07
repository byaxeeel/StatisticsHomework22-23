using Microsoft.VisualBasic.FileIO;
using System.Globalization;

namespace HW6
{
    public partial class Form1 : Form
    {
        Dictionary<int, double> datasetWeight;
        Dictionary<int, List<double>> samples;

        Random random = new Random();

        Bitmap b;
        Graphics g;

        Boolean boolWeight = true;
        Pen PenTrajectoryR = new Pen(Color.Blue, 2);

        Bitmap bIstogram;
        Graphics gIstogram;


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TextFieldParser parser = new TextFieldParser(@"../../../weight-height.csv");
            datasetWeight = new Dictionary<int, double>();
            samples = new Dictionary<int, List<double>>();
            this.b = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            this.g = Graphics.FromImage(b);
            this.g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.g.Clear(Color.White);


            int nSamples = (int)numericUpDown1.Value;
            int samplesSize = (int)numericUpDown2.Value;

            double minX = 0;
            double maxX = samplesSize;
            double maxValue = 0;
            double minValue = 0;
            double minY = minValue;
            double maxY = maxValue;

            Rectangle VirtualWindow = new Rectangle(0, 0, this.b.Width - 1, this.b.Height - 1);

            g.DrawRectangle(Pens.Black, VirtualWindow);

            List<double> populationValues = new List<double>();

            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            parser.ReadLine();
            int userId = 0;
            while (!parser.EndOfData)
            {
                string row = parser.ReadLine();
                int f = row.LastIndexOf(",") + 1;
                string attribute;
                if (boolWeight == false)
                {
                    string tempS = row.Substring(0, f-1);
                    int startFrom = tempS.LastIndexOf(",") + 1;
                    attribute = row.Substring(startFrom, f-startFrom-1);
                }
                else
                {
                    attribute = row.Substring(f);
                }

                double newData = convertData(boolWeight, double.Parse(attribute, CultureInfo.InvariantCulture));
                populationValues.Add(newData);
                datasetWeight[userId] = newData;
                userId++;
            }
            //dataset created into dict datasetWeight;
            
            //samples's distribution:

            List<double> lastAvg = new List<double>();
            List<double> lastAvgNormal = new List<double>();

            for (int i = 0; i < nSamples; i++)
            {
                List<int> skip = new List<int>();
                List<double> attributes = new List<double>();
                List<double> avgList = new List<double>();

                List<Point> Punti = new List<Point>();

                for (int x = 0; x <= samplesSize; x++)
                {
                    int randomNumber = random.Next(0, userId);
                    while (skip.Contains(randomNumber))
                    {
                       randomNumber = random.Next(0, userId);
                    }
                    skip.Add(randomNumber);
                    attributes.Add(datasetWeight[randomNumber]);
                    double avg = attributes.Average();
                    avgList.Add(avg);

                    if (minValue == 0 || avg < minValue)
                    {
                        minValue = avg;
                    }

                    if (minValue == 0 || avg > maxValue)
                    {
                        maxValue = avg;
                    }

                }

                minY = minValue;
                this.label4.Text = "min y: "+minY.ToString();
                this.label4.Visible = true;
                maxY = maxValue;
                this.label3.Text = "max y: "+maxY.ToString(); 
                this.label3.Visible = true;

                int coordX = 0;
                foreach (double avg in avgList)
                {
                    int xDevice = FromXRealToXVirtual(coordX, minX, maxX, VirtualWindow.Left, VirtualWindow.Width);
                    int yDevice = FromYRealToYVirtual(avg, minY, maxY, VirtualWindow.Top, VirtualWindow.Height);
                    Punti.Add(new Point(xDevice, yDevice));
                    coordX++;
                }
                samples[i] = attributes;
                lastAvgNormal.Add(avgList.Last());                
                lastAvg.Add(FromYRealToYVirtual(avgList.Last(), minY, maxY, VirtualWindow.Top, VirtualWindow.Height));
                this.richTextBox1.Text = "Population's Mean: " + populationValues.Average() + "\n" + "Samples's Mean: " + lastAvgNormal.Average();


                g.DrawLines(PenTrajectoryR, Punti.ToArray());
            }

            this.pictureBox1.Image = b;
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

        private double convertData(Boolean boolWeight, double number)
        {
            if (boolWeight) return number * 0.453592;
            else return number * 2.54;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (boolWeight) boolWeight = false;
            else boolWeight = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TextFieldParser parser = new TextFieldParser(@"../../../weight-height.csv");
            datasetWeight = new Dictionary<int, double>();
            samples = new Dictionary<int, List<double>>();
            this.b = new Bitmap(this.pictureBox4.Width, this.pictureBox4.Height);
            this.g = Graphics.FromImage(b);
            this.g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.g.Clear(Color.White);


            int nSamples = (int)numericUpDown1.Value;
            int samplesSize = (int)numericUpDown2.Value;

            double minX = 0;
            double maxX = samplesSize;
            double maxValue = 0;
            double minValue = 0;
            double minY = minValue;
            double maxY = maxValue;

            Rectangle VirtualWindow = new Rectangle(0, 0, this.b.Width - 1, this.b.Height - 1);

            g.DrawRectangle(Pens.Black, VirtualWindow);

            List<double> populationValues = new List<double>();

            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            parser.ReadLine();
            int userId = 0;
            while (!parser.EndOfData)
            {
                string row = parser.ReadLine();
                int f = row.LastIndexOf(",") + 1;
                string attribute;
                if (boolWeight == false)
                {
                    string tempS = row.Substring(0, f - 1);
                    int startFrom = tempS.LastIndexOf(",") + 1;
                    attribute = row.Substring(startFrom, f - startFrom - 1);
                }
                else
                {
                    attribute = row.Substring(f);
                }

                double newData = convertData(boolWeight, double.Parse(attribute, CultureInfo.InvariantCulture));
                populationValues.Add(newData);
                datasetWeight[userId] = newData;
                userId++;
            }
            //dataset created into dict datasetWeight;

            //samples's distribution:

            List<double> lastAvgNormal = new List<double>();

            for (int i = 0; i < nSamples; i++)
            {
                List<int> skip = new List<int>();
                List<double> attributes = new List<double>();
                List<double> avgList = new List<double>();

                List<Point> Punti = new List<Point>();

                for (int x = 0; x <= samplesSize; x++)
                {
                    int randomNumber = random.Next(0, userId);
                    while (skip.Contains(randomNumber))
                    {
                        randomNumber = random.Next(0, userId);
                    }
                    skip.Add(randomNumber);
                    attributes.Add(datasetWeight[randomNumber]);

                    double avg1 = attributes.Average();
                    double variance1 = 0.0;
                    foreach (int value in attributes)
                    {
                        variance1 += Math.Pow(value - avg1, 2.0);
                    }
                    double var1 = variance1 / attributes.Count; 

                    avgList.Add(var1);

                    if (minValue == 0 || var1 < minValue)
                    {
                        minValue = var1;
                    }

                    if (minValue == 0 || var1 > maxValue)
                    {
                        maxValue = var1;
                    }

                }

                minY = minValue;
                this.label6.Text = "min y: " + minY.ToString();
                this.label6.Visible = true;
                maxY = maxValue;
                this.label5.Text = "max y: " + maxY.ToString();
                this.label5.Visible = true;

                int coordX = 0;
                foreach (double avg in avgList)
                {
                    int xDevice = FromXRealToXVirtual(coordX, minX, maxX, VirtualWindow.Left, VirtualWindow.Width);
                    int yDevice = FromYRealToYVirtual(avg, minY, maxY, VirtualWindow.Top, VirtualWindow.Height);
                    Punti.Add(new Point(xDevice, yDevice));
                    coordX++;
                }
                samples[i] = attributes;
                lastAvgNormal.Add(avgList.Last());

                g.DrawLines(PenTrajectoryR, Punti.ToArray());
            }

            double avgPop = populationValues.Average();
            double variancePop = 0.0;
            foreach (int value in populationValues)
            {
                variancePop += Math.Pow(value - avgPop, 2.0);
            }
            double var = variancePop / populationValues.Count;

            this.richTextBox2.Text = "Population's Variance: "+ var.ToString() + "\n" + "Samples's Variance: "+lastAvgNormal.Average();

            this.pictureBox4.Image = b;

        }
    }
}