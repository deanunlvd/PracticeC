using System;
using System.Windows.Forms;

namespace ex3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string task = txtTask.Text.Trim(); // Получаем текст из TextBox и убираем лишние пробелы
            if (!string.IsNullOrEmpty(task)) // Проверяем, что текст не пустой
            {
                listBoxTasks.Items.Add(task); // Добавляем задачу в ListBox
                txtTask.Clear(); // Очищаем TextBox после добавления
            }
            else
            {
                MessageBox.Show("Введите задачу!"); // Сообщение, если задача пуста
            }
        }

        private void btnDlt_Click(object sender, EventArgs e)
        {
            if (listBoxTasks.SelectedItem != null) // Проверяем, что что-то выбрано
            {
                listBoxTasks.Items.Remove(listBoxTasks.SelectedItem); // Удаляем выбранную задачу
            }
            else
            {
                MessageBox.Show("Выберите задачу для удаления!"); // Сообщение, если ничего не выбрано
            }
        }

        private void listBoxTasks_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxTasks.SelectedItem != null) // Проверяем, что что-то выбрано
            {
                string selectedTask = listBoxTasks.SelectedItem.ToString(); // Получаем выбранную задачу
                listBoxTasks.Items[listBoxTasks.SelectedIndex] = $"✔️ {selectedTask}"; // Отмечаем задачу как выполненную
            }
        }

        private void btnDeleteCompleted_Click(object sender, EventArgs e)
        {
            for (int i = listBoxTasks.Items.Count - 1; i >= 0; i--) // Проходим по элементам в обратном порядке
            {
                if (listBoxTasks.Items[i].ToString().StartsWith("✔️")) // Проверяем, начинается ли задача с "✔️"
                {
                    listBoxTasks.Items.RemoveAt(i); // Удаляем выполненные задачи
                }
            }
        }
    }
}
