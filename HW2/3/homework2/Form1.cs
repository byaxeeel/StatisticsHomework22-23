using Microsoft.VisualBasic.FileIO;

namespace homework2
{
    public partial class Form1 : Form
    {
        TextFieldParser parser = new TextFieldParser(@"..\..\..\car_evaluation.csv");
        TextFieldParser parser2 = new TextFieldParser(@"..\..\..\car_evaluation.csv");

        string[] valori = {};
        Dictionary<string, int> valori2 = new Dictionary<string, int>();


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
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            while (!parser.EndOfData)
            {
                string row = parser.ReadLine();
                int inizioda = row.LastIndexOf(",") + 1;
                string scrivo = row.Substring(inizioda);
                if (scrivo == "6")
                {
                    continue;
                }
                else
                {
                    if (valori2.ContainsKey(scrivo) == false)
                    {
                        valori2.Add(scrivo,1);

                    }
                    else
                    {
                        valori2[scrivo] = valori2[scrivo] + 1;

                    }
                }
            }
            foreach (string key in valori2.Keys){
                this.richTextBox2.AppendText(key + " : " + valori2[key] + "\n");
            }
            parser = new TextFieldParser(@"..\..\..\car_evaluation.csv");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
            parser = new TextFieldParser(@"..\..\..\car_evaluation.csv");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.richTextBox2.Text = "";
            parser2 = new TextFieldParser(@"..\..\..\car_evaluation.csv");
        }
    }
}