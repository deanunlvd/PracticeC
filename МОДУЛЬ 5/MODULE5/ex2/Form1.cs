using System;
using System.IO;
using System.Windows.Forms;

namespace ex2
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void save(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Text File (*.txt)|*.txt";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFile.FileName;

                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        // Сохранение содержимого RichTextBox как обычный текст
                        writer.Write(richTextBox1.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении файла: " + ex.Message);
                }
            }
        }

        private void open(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Text Files (*.txt)|*.txt";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFile.FileName;

                try
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        // Загрузка содержимого из файла в RichTextBox
                        richTextBox1.Text = reader.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при открытии файла: " + ex.Message);
                }
            }
        }
    }
}