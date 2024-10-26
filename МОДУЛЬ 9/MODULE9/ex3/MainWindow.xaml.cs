using System;
using System.Collections.Generic;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;


namespace ex3
{
    public class Task
    {
        public string Name { get; set; }
        public string Priority { get; set; }
        public DateTime DueDate { get; set; }
        public int Progress { get; set; }
    }
    public partial class MainWindow : Window
    {
        private ObservableCollection<Task> tasks;
        private Task selectedTask;
        public MainWindow()
        {
            InitializeComponent();
            tasks = new ObservableCollection<Task>();
            TaskList.ItemsSource = tasks;

            TaskList.SelectionChanged += TaskList_SelectionChanged;
        }
        private void TaskList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TaskList.SelectedItem is Task task)
            {
                selectedTask = task;

                // Отображение данных задачи
                TaskNameInput.Text = task.Name;
                PriorityInput.SelectedItem = GetComboBoxItemByContent(PriorityInput, task.Priority);
                DueDateInput.SelectedDate = task.DueDate;
                ProgressSlider.Value = task.Progress;
            }
        }

        // Метод для получения ComboBoxItem по содержимому
        private ComboBoxItem GetComboBoxItemByContent(ComboBox comboBox, string content)
        {
            foreach (ComboBoxItem item in comboBox.Items)
            {
                if (item.Content.ToString() == content)
                    return item;
            }
            return null;
        }

        // Добавление новой задачи
        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            var task = new Task
            {
                Name = TaskNameInput.Text,
                Priority = (PriorityInput.SelectedItem as ComboBoxItem)?.Content.ToString(),
                DueDate = DueDateInput.SelectedDate ?? DateTime.Now,
                Progress = (int)ProgressSlider.Value
            };
            tasks.Add(task);
            ClearTaskInputs();
        }

        // Удаление выбранной задачи
        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTask != null)
            {
                tasks.Remove(selectedTask);
                ClearTaskInputs();
            }
        }

        // Обновление выбранной задачи
        private void UpdateTask_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTask != null)
            {
                selectedTask.Name = TaskNameInput.Text;
                selectedTask.Priority = (PriorityInput.SelectedItem as ComboBoxItem)?.Content.ToString();
                selectedTask.DueDate = DueDateInput.SelectedDate ?? DateTime.Now;
                selectedTask.Progress = (int)ProgressSlider.Value;

                TaskList.Items.Refresh(); // Обновление отображения списка
                ClearTaskInputs();
            }
        }

        // Очистка полей ввода
        private void ClearTaskInputs()
        {
            TaskNameInput.Clear();
            PriorityInput.SelectedIndex = -1;
            DueDateInput.SelectedDate = null;
            ProgressSlider.Value = 0;
        }
    }
}
    // Класс задачи
    public class Task
    {
        public string Name { get; set; }
        public string Priority { get; set; }
        public DateTime DueDate { get; set; }
        public int Progress { get; set; }
    }