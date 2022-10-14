using Microsoft.VisualBasic.FileIO;

namespace homework2
{
    public partial class Form1 : Form
    {
        TextFieldParser parser = new TextFieldParser(@"..\..\..\dataset.csv");
        TextFieldParser parser2 = new TextFieldParser(@"..\..\..\dataset.csv");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> value1 = new Dictionary<string, int>();
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            while (!parser.EndOfData)
            {
                string row = parser.ReadLine();

                int count = 0;
                int firstDel = 0;
                int secondDel = 0;
                for (int i = 0; i < row.Length; i++)
                {
                    if (row[i] == ',')
                    {
                        count++;
                        if (count == 3)
                        {
                            firstDel = i + 1;
                        }
                        if (count == 4)
                        {
                            secondDel = i;
                            break;
                        }
                    }
                }

                string scrivo = row.Substring(firstDel, secondDel - firstDel);

                if (value1.ContainsKey(scrivo) == false)
                {
                    value1.Add(scrivo, 1);

                }
                else
                {
                    value1[scrivo] = value1[scrivo] + 1;
                }

            }
            foreach (string key in value1.Keys)
            {
                this.richTextBox1.AppendText(key + " : " + value1[key] + "\n");
            }
            parser = new TextFieldParser(@"..\..\..\dataset.csv");

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> value2 = new Dictionary<string, int>();
            parser2.TextFieldType = FieldType.Delimited;
            parser2.SetDelimiters(",");
            while (!parser2.EndOfData)
            {
                string row = parser2.ReadLine();

                int count = 0;
                int firstDel = 0;
                int secondDel = 0;
                for (int i = 0; i < row.Length; i++)
                {
                    if (row[i] == ',')
                    {
                        count++;
                        if (count == 4)
                        {
                            firstDel = i+1;
                        }
                        if (count == 5)
                        {
                            secondDel = i;
                            break;
                        }
                    }
                }

                string scrivo = row.Substring(firstDel,secondDel-firstDel);

                    if (value2.ContainsKey(scrivo) == false)
                    {
                        value2.Add(scrivo,1);

                    }
                    else
                    {
                        value2[scrivo] = value2[scrivo] + 1;
                    }
                
            }
            foreach (string key in value2.Keys){
                this.richTextBox2.AppendText(key + " : " + value2[key] + "\n");
            }
            parser2 = new TextFieldParser(@"..\..\..\dataset.csv");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
            parser = new TextFieldParser(@"..\..\..\dataset.csv");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.richTextBox2.Text = "";
            parser2 = new TextFieldParser(@"..\..\..\dataset.csv");

        }
    }
}