using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ex_4
{
    // Класс для представления данных
    public class DataItem
    {
        // Свойства: дата и описание элемента данных
        public DateTime Date { get; set; }
        public string Description { get; set; }

        // Конструктор для инициализации элемента данных
        public DataItem(DateTime date, string description)
        {
            Date = date;
            Description = description;
        }

        // Переопределение метода для отображения данных
        public override string ToString()
        {
            return $"{Date.ToShortDateString()} - {Description}";
        }
    }

    public partial class MainWindow : Window
    {
        // Список всех элементов данных
        private List<DataItem> allDataItems;

        // Список фильтрованных элементов данных
        private List<DataItem> filteredDataItems;

      
        public MainWindow()
        {
            InitializeComponent();  
            LoadData();  // Загрузка данных при запуске
        }

        // Метод для загрузки данных
        private void LoadData()
        {
            // Инициализация списка всех элементов данных
            allDataItems = new List<DataItem>
            {
                new DataItem(new DateTime(2024, 10, 1), "Отчет о продажах"),
                new DataItem(new DateTime(2024, 10, 2), "Собрание команды"),
                new DataItem(new DateTime(2024, 10, 1), "Подготовка документов"),
                new DataItem(new DateTime(2024, 10, 3), "Встреча с руководителем"),
                new DataItem(new DateTime(2024, 10, 3), "Обед"),
                new DataItem(new DateTime(2024, 10, 3), "Завтрак"),
                new DataItem(new DateTime(2024, 10, 3), "Ужиин"),
            };

            // Копируем все элементы в список фильтрованных данных
            filteredDataItems = new List<DataItem>(allDataItems);

            // Обновляем интерфейс для отображения данных
            UpdateListBox();
        }

        // Метод для обновления списка данных в интерфейсе
        private void UpdateListBox()
        {
            // Обнуляем источник данных для ListBox
            DataListBox.ItemsSource = null;

            // Устанавливаем новый источник данных - фильтрованный список
            DataListBox.ItemsSource = filteredDataItems;
        }

        // Метод для применения фильтра по ключевому слову
        private void OnApplyKeywordFilter(object sender, RoutedEventArgs e)
        {
            // Получаем ключевое слово из текстового поля
            string keyword = KeywordTextBox.Text;

            // Если ключевое слово не пустое, фильтруем список данных
            if (!string.IsNullOrEmpty(keyword))
            {
                // Используем делегат для фильтрации по ключевому слову
                filteredDataItems = allDataItems
                    .Where(item => Filters.FilterByKeyword(item, keyword))
                    .ToList();

                // Обновляем интерфейс для отображения отфильтрованных данных
                UpdateListBox();
            }
        }

        // Метод для сброса фильтра и возврата всех данных
        private void OnResetFilter(object sender, RoutedEventArgs e)
        {
            // Возвращаем все элементы данных в фильтрованный список
            filteredDataItems = new List<DataItem>(allDataItems);

            // Обновляем интерфейс для отображения всех данных
            UpdateListBox();
        }
    }

    // Статический класс, содержащий методы фильтрации
    public static class Filters
    {
        // Метод для фильтрации по ключевому слову в описании
        public static bool FilterByKeyword(DataItem item, string keyword)
        {
            // Проверяем, содержит ли описание элемента ключевое слово
            return item.Description.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
