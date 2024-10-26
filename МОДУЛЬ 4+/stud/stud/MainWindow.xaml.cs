using System;
using System.Linq;
using System.Windows;
namespace stud
{
    public interface IStudent
    {
        double GetAverageGrade();  // Метод для определения среднего балла
        string GetCourseInfo();    // Метод для получения информации о курсе
    }
    public class StudentFirstYear : IStudent
    {
        private string name;
        private double[] grades;
        public StudentFirstYear(string name, double[] grades)
        {
            this.name = name;
            this.grades = grades;
        }
        public double GetAverageGrade()
        {
            double sum = grades.Sum();
            return sum / grades.Length;
        }
        public string GetCourseInfo()
        {
            return $"{name} - студент 1-го курса.";
        }
    }
    public class StudentSecondYear : IStudent
    {
        private string name;
        private double[] grades;
        public StudentSecondYear(string name, double[] grades)
        {
            this.name = name;
            this.grades = grades;
        }
        public double GetAverageGrade()
        {
            double sum = grades.Sum();
            return sum / grades.Length;
        }
        public string GetCourseInfo()
        {
            return $"{name} - студент 2-го курса.";
        }
    }
    public class StudentThirdYear : IStudent
    {
        private string name;
        private double[] grades;
        public StudentThirdYear(string name, double[] grades)
        {
            this.name = name;
            this.grades = grades;
        }
        public double GetAverageGrade()
        {
            double sum = grades.Sum();
            return sum / grades.Length;
        }
        public string GetCourseInfo()
        {
            return $"{name} - студент 3-го курса.";
        }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void OnCalculateClick(object sender, RoutedEventArgs e)
        {
            // Получаем имя студента
            string name = NameTextBox.Text;
            // Проверка на наличие ФИО
            if (string.IsNullOrWhiteSpace(name) || !System.Text.RegularExpressions.Regex.IsMatch(name, @"^[А-ЯЁа-яё\s]+$"))
            {
                MessageBox.Show("Пожалуйста, введите корректное ФИО (только буквы и пробелы).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Получаем оценки студента и преобразуем их в массив
            string gradesInput = GradesTextBox.Text;
            double[] grades;
            try
            {
                grades = gradesInput.Split(',')
                                     .Select(g => Convert.ToDouble(g.Trim()))
                                     .ToArray();
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректные оценки (числа).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Проверка на максимальные оценки
            if (grades.Any(g => g < 0 || g > 10))
            {
                MessageBox.Show("Оценки должны быть в диапазоне от 0 до 10.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Получаем выбранный курс
            int selectedIndex = CourseComboBox.SelectedIndex;
            IStudent student;
            // Создаем студента в зависимости от выбранного курса
            switch (selectedIndex)
            {
                case 0:
                    student = new StudentFirstYear(name, grades);
                    break;
                case 1:
                    student = new StudentSecondYear(name, grades);
                    break;
                case 2:
                    student = new StudentThirdYear(name, grades);
                    break;
                default:
                    CourseInfoTextBlock.Text = "Пожалуйста, выберите курс.";
                    return;
            }
            // Выводим информацию о курсе и среднем балле
            CourseInfoTextBlock.Text = student.GetCourseInfo();
            AverageGradeTextBlock.Text = student.GetAverageGrade().ToString("F2");
        }
    }
}
