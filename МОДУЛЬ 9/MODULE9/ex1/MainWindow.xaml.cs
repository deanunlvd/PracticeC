using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Linq;

namespace ex1
{
    public partial class MainWindow : Window
    {
        private const string ConnectionString = "server=localhost;database=StudentManagement;uid=root;pwd=korol783;";
        private int nextId = 1;
        private List<Student> students = new List<Student>();
        public MainWindow()
        {
            InitializeComponent();
            LoadStudentsFromDatabase();
        }
        private void LoadStudentsFromDatabase()
        {
            students.Clear();
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Students";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var student = new Student
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Age = reader.GetInt32(2),
                        Major = reader.GetString(3)
                    };
                    students.Add(student);
                }
            }
            UpdateStudentList();
            nextId = students.Count > 0 ? students[students.Count - 1].Id + 1 : 1;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var student = new Student
            {
                Id = nextId++,
                Name = txtName.Text,
                Age = int.TryParse(txtAge.Text, out int age) ? age : 0,
                Major = txtMajor.Text
            };

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "INSERT INTO Students (Name, Age, Major) VALUES (@Name, @Age, @Major)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", student.Name);
                command.Parameters.AddWithValue("@Age", student.Age);
                command.Parameters.AddWithValue("@Major", student.Major);
                command.ExecuteNonQuery();
            }

            students.Add(student);
            UpdateStudentList();
            ClearInputs();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lstStudents.SelectedItem is Student selectedStudent)
            {
                selectedStudent.Name = txtName.Text;
                selectedStudent.Age = int.TryParse(txtAge.Text, out int age) ? age : 0;
                selectedStudent.Major = txtMajor.Text;

                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "UPDATE Students SET Name = @Name, Age = @Age, Major = @Major WHERE Id = @Id";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", selectedStudent.Name);
                    command.Parameters.AddWithValue("@Age", selectedStudent.Age);
                    command.Parameters.AddWithValue("@Major", selectedStudent.Major);
                    command.Parameters.AddWithValue("@Id", selectedStudent.Id);
                    command.ExecuteNonQuery();
                }

                UpdateStudentList();
                ClearInputs();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstStudents.SelectedItem is Student selectedStudent)
            {
                students.Remove(selectedStudent);

                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Students WHERE Id = @Id";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", selectedStudent.Id);
                    command.ExecuteNonQuery();
                }

                UpdateStudentList();
                ClearInputs();
            }
        }

        private void lstStudents_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstStudents.SelectedItem is Student selectedStudent)
            {
                txtId.Text = selectedStudent.Id.ToString();
                txtName.Text = selectedStudent.Name;
                txtAge.Text = selectedStudent.Age.ToString();
                txtMajor.Text = selectedStudent.Major;
            }
        }

        private void UpdateStudentList()
        {
            lstStudents.ItemsSource = null;
            lstStudents.ItemsSource = students;
        }

        private void ClearInputs()
        {
            txtId.Clear();
            txtName.Clear();
            txtAge.Clear();
            txtMajor.Clear();
        }

        public class Student
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public string Major { get; set; }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();
            var filteredStudents = students.Where(s => s.Name.ToLower().Contains(searchText)).ToList();
            lstStudents.ItemsSource = filteredStudents;
        }

        private void btnSortByAge_Click(object sender, RoutedEventArgs e)
        {
            var sortedStudents = students.OrderBy(s => s.Age).ToList();
            lstStudents.ItemsSource = sortedStudents;
        }

        private void btnSortByName_Click(object sender, RoutedEventArgs e)
        {
            var sortedStudents = students.OrderBy(s => s.Name).ToList();
            lstStudents.ItemsSource = sortedStudents;
        }
    }
}
