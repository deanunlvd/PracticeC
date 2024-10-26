using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Threading.Tasks;
using System.Windows;

namespace ex4
{
    public partial class MainWindow : Window
    {
        private string connectionString = "Server=localhost;Database=FinanceDB;User Id=root;Password=korol783;";
        public MainWindow()
        {
            InitializeComponent();
            LoadIncome();
            LoadExpenses();
        }
        private void LoadIncome()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM Income", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridIncome.ItemsSource = dataTable.DefaultView; // Исправлено здесь
            }
        }

        private void LoadExpenses()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM Expense", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridExpenses.ItemsSource = dataTable.DefaultView; // Исправлено здесь
            }
        }

        private void btnAddIncome_Click(object sender, RoutedEventArgs e)
        {
            AddEditForm addEditForm = new AddEditForm(true);
            if (addEditForm.ShowDialog() == true) // Изменено на 'true'
            {
                LoadIncome(); // Обновляем данные после добавления
            }
        }

        private void btnAddExpense_Click(object sender, RoutedEventArgs e)
        {
            AddEditForm addEditForm = new AddEditForm(false);
            if (addEditForm.ShowDialog() == true) // Изменено на 'true'
            {
                LoadExpenses(); // Обновляем данные после добавления
            }
        }
    }
}