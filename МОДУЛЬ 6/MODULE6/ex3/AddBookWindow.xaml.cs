using MySql.Data.MySqlClient;
using System.Windows;

namespace ex3
{

    public partial class AddBookWindow : Window
    {
        private string connectionString = "Server=localhost;Database=LibraryDB;User Id=root;Password=korol783;";
        public AddBookWindow()
        {
            InitializeComponent();
        }
        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text;
            string author = txtAuthor.Text;

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Books (Title, Author) VALUES (@Title, @Author)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Author", author);

                command.ExecuteNonQuery();
                MessageBox.Show("Книга успешно добавлена.");
            }

            this.Close();  // Закрываем окно после добавления книги
        }
    }
}