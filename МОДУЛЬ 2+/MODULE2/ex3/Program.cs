using System;

class Author
{
    // Поля для хранения информации об авторе
    public string Name { get; set; }
    public int BirthYear { get; set; }

    // Конструктор для инициализации автора
    public Author(string name, int birthYear)
    {
        Name = name;
        BirthYear = birthYear;
    }
}

class Book
{
    // Поля для хранения информации о книге
    public string Title { get; set; }
    public int Year { get; set; }
    public Author Author { get; set; } // Связь с классом Author

    // Конструктор для инициализации книги
    public Book(string title, int year, Author author)
    {
        Title = title;
        Year = year;
        Author = author;
    }
}

class Program
{
    static void Main()
    {
        // Создание объектов Author
        Author author1 = new Author("Лев Толстой", 1828);
        Author author2 = new Author("Федор Достоевский", 1821);

        // Создание объектов Book
        Book book1 = new Book("Война и мир", 1869, author1);
        Book book2 = new Book("Преступление и наказание", 1866, author2);

        // Вывод информации об авторах и книгах
        Console.WriteLine($"Книга: \"{book1.Title}\", Год выпуска: {book1.Year}, Автор: {book1.Author.Name}, Год рождения автора: {book1.Author.BirthYear}");
        Console.WriteLine($"Книга: \"{book2.Title}\", Год выпуска: {book2.Year}, Автор: {book2.Author.Name}, Год рождения автора: {book2.Author.BirthYear}");
    }
}
