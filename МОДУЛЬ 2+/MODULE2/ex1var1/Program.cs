using System;

class Student
{
    // Свойства студента
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public double AverageGrade { get; set; }

    // Конструктор класса
    public Student(string firstName, string lastName, int age, double averageGrade)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        AverageGrade = averageGrade;
    }

    // Метод для вывода информации о студенте
    public void DisplayInfo()
    {
        Console.WriteLine($"Имя: {FirstName}, Фамилия: {LastName}, Возраст: {Age}, Средний балл: {AverageGrade:F2}");
    }
}

class Program
{
    static void Main()
    {
        // Создаем экземпляры класса Student
        Student student1 = new Student("Иван", "Иванов", 20, 4.5);
        Student student2 = new Student("Мария", "Петрова", 22, 3.8);
        Student student3 = new Student("Алексей", "Сидоров", 21, 4.9);

        // Вывод информации о студентах
        student1.DisplayInfo();
        student2.DisplayInfo();
        student3.DisplayInfo();
    }
}
