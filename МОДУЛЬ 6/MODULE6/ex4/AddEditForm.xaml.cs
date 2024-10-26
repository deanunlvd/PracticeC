using System;
using System.Windows;
using MySql.Data.MySqlClient;

namespace ex4
{
    public partial class AddEditForm : Window
    {
        private string connectionString = "Server=localhost;Database=FinanceDB;User Id=root;Password=korol783;";
        private bool isIncome; // True, если это доход, иначе - расход
        private int? recordId; // Идентификатор записи для редактирования

        public AddEditForm(bool isIncome, int? id = null)
        {
            InitializeComponent();
            this.isIncome = isIncome;
            this.recordId = id;

            if (id.HasValue)
            {
                LoadRecord(id.Value);
            }
        }
        private void LoadRecord(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string table = isIncome ? "Income" : "Expense";
                MySqlCommand command = new MySqlCommand($"SELECT * FROM {table} WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    txtAmount.Text = reader["Amount"].ToString();
                    txtSourceOrCategory.Text = reader[isIncome ? "Source" : "Category"].ToString();
                    datePicker.SelectedDate = Convert.ToDateTime(reader["Date"]); // Исправлено здесь
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            decimal amount = Convert.ToDecimal(txtAmount.Text);
            string sourceOrCategory = txtSourceOrCategory.Text;
            DateTime date = datePicker.SelectedDate ?? DateTime.Now; // Используем значение по умолчанию, если ничего не выбрано

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query;

                if (recordId.HasValue) // Если редактируем
                {
                    string table = isIncome ? "Income" : "Expense";
                    query = $"UPDATE {table} SET Amount = @Amount, {(isIncome ? "Source" : "Category")} = @SourceOrCategory, Date = @Date WHERE Id = @Id";
                }
                else // Если добавляем
                {
                    string table = isIncome ? "Income" : "Expense";
                    query = $"INSERT INTO {table} (Amount, {(isIncome ? "Source" : "Category")}, Date) VALUES (@Amount, @SourceOrCategory, @Date)";
                }

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Amount", amount);
                command.Parameters.AddWithValue("@SourceOrCategory", sourceOrCategory);
                command.Parameters.AddWithValue("@Date", date);
                if (recordId.HasValue)
                {
                    command.Parameters.AddWithValue("@Id", recordId.Value);
                }

                command.ExecuteNonQuery();
            }

            this.DialogResult = true; // Изменено на 'true'
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}