using Microsoft.VisualBasic.FileIO;

namespace homework2
{
    public partial class Form1 : Form
    {
        TextFieldParser parser = new TextFieldParser(@"../../../car_evaluation.csv");
        TextFieldParser parser2 = new TextFieldParser(@"../../../car_evaluation.csv");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            while (!parser.EndOfData)
            {
                string row = parser.ReadLine();
                this.richTextBox1.AppendText(row + "\n");
            }

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            parser2.TextFieldType = FieldType.Delimited;
            parser2.SetDelimiters(",");
            string row = parser2.ReadLine();
            this.richTextBox2.AppendText(row + "\n");       
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
            parser = new TextFieldParser(@"../../../car_evaluation.csv");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.richTextBox2.Text = "";
            parser2 = new TextFieldParser(@"../../../car_evaluation.csv");
        }
    }
}