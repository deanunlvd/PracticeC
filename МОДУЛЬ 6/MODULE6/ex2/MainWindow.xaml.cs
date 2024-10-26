using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace ex2
{
    public partial class MainWindow : Window
    {
        private Task selectedTask;
        private string connectionString = "Server=localhost;Database=TaskManagement;User Id=root;Password=korol783;";
        public MainWindow()
        {
            InitializeComponent();
            LoadTasks();
        }
        private void LoadTasks()
        {
            List<Task> tasks = new List<Task>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Tasks";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        tasks.Add(new Task
                        {
                            TaskID = (int)reader["TaskID"],
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            DueDate = reader["DueDate"] as DateTime?,
                            Status = reader["Status"].ToString()
                        });
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }

            dataGridTasks.ItemsSource = tasks;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text;
            string description = txtDescription.Text;
            DateTime? dueDate = dpDueDate.SelectedDate;
            string status = ((ComboBoxItem)cbStatus.SelectedItem).Content.ToString();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Tasks (Title, Description, DueDate, Status) VALUES (@Title, @Description, @DueDate, @Status)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@DueDate", (object)dueDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Status", status);

                    command.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Ошибка при добавлении задачи: {ex.Message}");
                }
            }

            LoadTasks();
            txtTitle.Clear();
            txtDescription.Clear();
            dpDueDate.SelectedDate = null;
            cbStatus.SelectedIndex = 0;
        }

        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTask == null)
            {
                MessageBox.Show("Сначала выберите задачу для редактирования.");
                return;
            }

            string title = txtTitle.Text;
            string description = txtDescription.Text;
            DateTime? dueDate = dpDueDate.SelectedDate;
            string status = ((ComboBoxItem)cbStatus.SelectedItem)?.Content.ToString();

            // Проверка длины статусной строки
            if (status.Length > 20) // Предположим, что максимальная длина 20 символов
            {
                MessageBox.Show("Статус не должен превышать 20 символов.");
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Tasks SET Title = @Title, Description = @Description, DueDate = @DueDate, Status = @Status WHERE TaskID = @TaskID";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@DueDate", (object)dueDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@TaskID", selectedTask.TaskID);

                    command.ExecuteNonQuery();
                    LoadTasks(); // обновляем список задач
                    ClearFields(); // очищаем поля
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Ошибка при редактировании задачи: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                }
            }
        }
    


    private void ClearFields()
        {
            txtTitle.Clear();
            txtDescription.Clear();
            dpDueDate.SelectedDate = null;
            cbStatus.SelectedIndex = -1;
            selectedTask = null; // сбрасываем выбранную задачу
        }

        private void dataGridTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridTasks.SelectedItem is Task task)
            {
                selectedTask = task;
                txtTitle.Text = task.Title;
                txtDescription.Text = task.Description;
                dpDueDate.SelectedDate = task.DueDate;
                cbStatus.SelectedItem = cbStatus.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == task.Status);
            }
            else
            {
                ClearFields(); // Очищаем поля, если ничего не выбрано
            }
        }
    }

    public class Task
    {
        public int TaskID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; }
    }
}