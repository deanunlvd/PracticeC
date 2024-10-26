using System;
using System.Collections.Generic;
using System.Windows;
using MySql.Data.MySqlClient;

namespace ex3
{
    public partial class MainWindow : Window
    {
        private string connectionString = "Server=localhost;Database=LibraryDB;User Id=root;Password=korol783;";
        public MainWindow()
        {
            InitializeComponent();
            LoadBooks("");
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchTitle = txtSearch.Text.Trim();
            LoadBooks(searchTitle);
        }

        private void LoadBooks(string title)
        {
            List<Book> books = new List<Book>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Title, Author FROM Books WHERE Title LIKE @Title";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", "%" + title + "%");

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    books.Add(new Book
                    {
                        Title = reader["Title"].ToString(),
                        Author = reader["Author"].ToString()
                    });
                }
            }

            dataGridBooks.ItemsSource = books;
        }


        // Открытие нового окна для добавления книги
        private void btnOpenAddBookWindow_Click(object sender, RoutedEventArgs e)
        {
            AddBookWindow addBookWindow = new AddBookWindow();
            addBookWindow.ShowDialog();
            LoadBooks(""); // Обновляем список книг после закрытия окна
        }

        // Функция для аренды книги
        private void btnRent_Click(object sender, RoutedEventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Пожалуйста, введите имя и email.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Логика аренды книги
            MessageBox.Show($"Книга арендована пользователем {userName}.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public class Book
        {
            public int BookID { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
        }

    }
}