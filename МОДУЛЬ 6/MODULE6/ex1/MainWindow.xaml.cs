using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;


namespace ex1
{
    public partial class MainWindow : Window
    {
        private string connectionString = "server=localhost;database=EmployeeManagement;user=root;password=korol783;";
        public MainWindow()
        {
            InitializeComponent();
            LoadEmployees();
        }
        private void LoadEmployees()
        {
            string query = "SELECT * FROM Employees";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                EmployeesDataGrid.ItemsSource = dt.DefaultView;
            }
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            string query = "INSERT INTO Employees (FirstName, LastName, Position, Department, Phone, Email, HireDate) VALUES (@FirstName, @LastName, @Position, @Department, @Phone, @Email, @HireDate)";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", FirstNameTextBox.Text);
                cmd.Parameters.AddWithValue("@LastName", LastNameTextBox.Text);
                cmd.Parameters.AddWithValue("@Position", PositionTextBox.Text);
                cmd.Parameters.AddWithValue("@Department", DepartmentTextBox.Text);
                cmd.Parameters.AddWithValue("@Phone", PhoneTextBox.Text);
                cmd.Parameters.AddWithValue("@Email", EmailTextBox.Text);
                cmd.Parameters.AddWithValue("@HireDate", HireDatePicker.SelectedDate.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadEmployees();
        }

        private void UpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите сотрудника для сохранения изменений.");
                return;
            }

            DataRowView row = (DataRowView)EmployeesDataGrid.SelectedItem;
            int employeeID = Convert.ToInt32(row["EmployeeID"]);

            string query = "UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, Position = @Position, Department = @Department, Phone = @Phone, Email = @Email, HireDate = @HireDate WHERE EmployeeID = @EmployeeID";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.Parameters.AddWithValue("@FirstName", FirstNameTextBox.Text);
                cmd.Parameters.AddWithValue("@LastName", LastNameTextBox.Text);
                cmd.Parameters.AddWithValue("@Position", PositionTextBox.Text);
                cmd.Parameters.AddWithValue("@Department", DepartmentTextBox.Text);
                cmd.Parameters.AddWithValue("@Phone", PhoneTextBox.Text);
                cmd.Parameters.AddWithValue("@Email", EmailTextBox.Text);
                cmd.Parameters.AddWithValue("@HireDate", HireDatePicker.SelectedDate.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadEmployees();
            MessageBox.Show("Изменения сохранены.");
        }



        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите сотрудника для удаления.");
                return;
            }

            DataRowView row = (DataRowView)EmployeesDataGrid.SelectedItem;
            int employeeID = Convert.ToInt32(row["EmployeeID"]);

            string query = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadEmployees();
        }

        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите сотрудника для редактирования.");
                return;
            }

            DataRowView row = (DataRowView)EmployeesDataGrid.SelectedItem;

            // Заполняем текстовые поля данными из выбранной строки
            FirstNameTextBox.Text = row["FirstName"].ToString();
            LastNameTextBox.Text = row["LastName"].ToString();
            PositionTextBox.Text = row["Position"].ToString();
            DepartmentTextBox.Text = row["Department"].ToString();
            PhoneTextBox.Text = row["Phone"].ToString();
            EmailTextBox.Text = row["Email"].ToString();

            // Проверяем наличие даты и присваиваем ее DatePicker
            if (row["HireDate"] != DBNull.Value)
            {
                HireDatePicker.SelectedDate = Convert.ToDateTime(row["HireDate"]);
            }
            else
            {
                HireDatePicker.SelectedDate = null;
            }
        }
    }
}
