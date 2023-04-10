namespace ArinaCourseApp
{
    public partial class Form1 : Form
    {

        public Form1()
        {

            InitializeComponent();
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt|Все файлы (*.*)|*.*";
                openFileDialog.InitialDirectory = @"C:\";
                openFileDialog.Title = "Выберите файл";
                openFileDialog.CheckFileExists = true;
                openFileDialog.CheckPathExists = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = openFileDialog.FileName;
                }
            }

        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                MessageBox.Show("Вы нажали Enter!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPress += new KeyPressEventHandler(button1_KeyPress);
        }

    }
}