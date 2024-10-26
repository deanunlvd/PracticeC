using System;
using System.Drawing;
using System.Windows.Forms;

namespace ex4
{
    public partial class Form1 : Form
    {
        private string imagePath;
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }

        private void trackBarScale_Scroll(object sender, EventArgs e)
        {
            UpdateImageScale();
        }

        private void open(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif" // Фильтр для выбора изображений
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imagePath = openFileDialog.FileName; // Получаем путь к выбранному изображению
                pictureBox.Image = new Bitmap(imagePath); // Загружаем изображение в PictureBox
                trackBarScale.Value = 100; // Сбросим масштаб
                UpdateImageScale(); // Обновим масштаб изображения
            }
        }
        private void UpdateImageScale()
        {
            if (pictureBox.Image != null)
            {
                float scaleFactor = trackBarScale.Value / 100f; // Получаем масштаб в процентах
                int newWidth = (int)(pictureBox.Image.Width * scaleFactor); // Новый размер по ширине
                int newHeight = (int)(pictureBox.Image.Height * scaleFactor); // Новый размер по высоте

                // Создаем новый Bitmap с измененным размером
                Bitmap resizedImage = new Bitmap(pictureBox.Image, newWidth, newHeight);

                // Устанавливаем новый Bitmap в PictureBox
                pictureBox.Image = resizedImage;
                lblScale.Text = $"Масштаб: {trackBarScale.Value}%"; // Обновляем текст с масштабом
            }
        }
    }
}
