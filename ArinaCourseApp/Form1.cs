using System.IO;
namespace ArinaCourseApp
{
    public partial class Form1 : Form
    {

        public Form1()
        {

            InitializeComponent();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = openFileDialog.FileName;
                // чтение данных из файла и их передача в программу
                using (StreamReader sr = new StreamReader(openFileDialog.FileName))
                {
                    string input = sr.ReadToEnd(); // read the string from file
                    string[] rows = input.Split('\n'); 
                    int[,] array = new int[rows.Length, rows[0].Split(',').Length];

                    for (int i = 0; i < rows.Length; i++)
                    {
                        string[] columns = rows[i].Split(',');
                        for (int j = 0; j < columns.Length; j++)
                        {
                            int value;
                            if (int.TryParse(columns[j], out value))
                            {
                                array[i, j] = value;
                            }
                        }
                    }
                    Alg mySol = new Alg(9, array);

                    mySol.DoSmth(array);
                    // обработка полученных данных
                }
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            /* StreamReader reader = new StreamReader("C://Users//user//source//repos//ArinaCourseApp//ArinaCourseApp//bin//Debug//net6.0-windows//test.txt", System.Text.Encoding.UTF8);
             string fileContent = reader.ReadToEnd();
             reader.Close();
             textBox4.Text = fileContent;*/
            /*string filePath = "C://Users//user//source//repos//ArinaCourseApp//ArinaCourseApp//bin//Debug//net6.0-windows//test.txt";
            string fileContents = File.ReadAllText(filePath);
            richTextBox1.Text = fileContents;*/
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = @"C:\";
            fileDialog.Filter = "Text Files|*.txt|All Files|*.*";
            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string fileContents = File.ReadAllText(fileDialog.FileName);
            richTextBox1.Text = fileContents;
        }

    }
}