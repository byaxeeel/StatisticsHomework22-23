using System.Diagnostics.Metrics;
using System.Windows.Forms;

namespace HW5._2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        Random rnd = new Random();
        Pen PenTrajectoryR = new Pen(Color.Blue, 2);
        List<Point> Punti = new List<Point>();
        Bitmap b;
        Graphics g;
        Graphics gZoom;

        Dictionary<int, List<double>> intervals = new Dictionary<int, List<double>>();

        int x_down;
        int y_down;

        int x_mouse;
        int y_mouse;

        int r_width;
        int r_height;

        Rectangle r;

        bool drag = false;
        bool resizing = false;

        bool distr = false;



        private void button1_Click(object sender, EventArgs e)
        {
            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(b);

            r = new Rectangle(20, 20, 500, 300);
            redraw(r, g);

            g.DrawRectangle(Pens.Black, r);
            this.pictureBox1.Image = b;
            this.button2.Visible = true;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (r.Contains(e.X, e.Y))
            {
                x_mouse = e.X;
                y_mouse = e.Y;

                x_down = r.X;
                y_down = r.Y;

                r_width = r.Width;
                r_height = r.Height;

                if (e.Button == MouseButtons.Left)
                {
                    drag = true;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    resizing = true;
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (distr == true)
            {
                redrawDistr(true);
            }
            drag = false;
            resizing = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int delta_x = e.X - x_mouse;
            int delta_y = e.Y - y_mouse;

            if (r != null)
            {
                if (drag)
                {

                    r.X = x_down + delta_x;
                    r.Y = y_down + delta_y;

                    redraw(r, g);
                }
                else if (resizing)
                {
                    r.Width = r_width + delta_x;
                    r.Height = r_height + delta_y;

                    redraw(r, g);

                }
            }
        }

        private void redraw(Rectangle r, Graphics g)
        {
            g.Clear(Color.White);
            g.DrawRectangle(Pens.Black, r);
            this.pictureBox1.Image = b;

        }

        private void redrawDistr(Boolean distr)
        {
            int Trials = 1000;
            int NumberOfTrajectories = 100;
            double SuccessProbability = 0.5;

            double minX = 0;
            double maxX = Trials;
            double minY = 0;
            double maxY = Trials;

            for (int i = 0; i < NumberOfTrajectories; i++)
            {
                Punti = new List<Point>();
                double y = 0;
                for (int x = 0; x <= Trials; x++)
                {
                    double cointoss = intervals[i].ElementAt(x);
                    intervals[i].Append(cointoss);
                    if (cointoss <= SuccessProbability)
                    {
                        y = y + 1;
                    }
                    double yRelative = y * Trials / (x + 1);
                    int xDevice = FromXRealToXVirtual(x, minX, maxX, r.Left, r.Width);
                    int yDevice = FromYRealToYVirtual(yRelative, minY, maxY, r.Top, r.Height);
                    Punti.Add(new Point(xDevice, yDevice));
                }
                g.DrawLines(PenTrajectoryR, Punti.ToArray());
            }
            this.pictureBox1.Image = b;

        }

        private void Form1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (distr == false) {
                distr = true;
                Distribution(true);
            }
            else
            {
                distr = false;
                Distribution(false);
            }
        }

        private void Distribution(bool on)
        {
            if (on == false)
            {
                redraw(r,g);
            }
            else {

                intervals = new Dictionary<int, List<double>>();

                Punti = new List<Point>();
                distr = true;
                int Trials = 1000;
                int NumberOfTrajectories = 100;
                double SuccessProbability = 0.5;

                double minX = 0;
                double maxX = Trials;
                double minY = 0;
                double maxY = Trials;

                for (int i = 0; i < NumberOfTrajectories; i++)
                {
                    intervals[i] = new List<double>();
                    Punti = new List<Point>();
                    double y = 0;
                    for (int x = 0; x <= Trials; x++)
                    {
                        double cointoss = rnd.NextDouble();
                        intervals[i].Add(cointoss);
                        if (cointoss <= SuccessProbability)
                        {
                            y = y + 1;
                        }
                        double yRelative = y * Trials / (x + 1);
                        int xDevice = FromXRealToXVirtual(x, minX, maxX, r.Left, r.Width);
                        int yDevice = FromYRealToYVirtual(yRelative, minY, maxY, r.Top, r.Height);
                        Punti.Add(new Point(xDevice, yDevice));

                    }
                    g.DrawLines(PenTrajectoryR, Punti.ToArray());
                }
                this.pictureBox1.Image = b;
            }

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

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                r = new Rectangle((int) (e.X - 1.25 * (e.X - r.X)), (int)(e.Y - 1.25 * (e.Y - r.Y)), (int)(r.Width * 1.25), (int)(r.Height * 1.25));

                redraw(r, g);

                if (distr == true)
                {
                    redrawDistr(this.distr);
                }

            }
            else
            {
                r = new Rectangle((int)(e.X - 0.75 * (e.X - r.X)), (int)(e.Y - 0.75 * (e.Y - r.Y)),(int)(r.Width * 0.75), (int)(r.Height * 0.75));
               
                redraw(r, g);

                if (distr == true)
                {
                    redrawDistr(this.distr);
                }
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}