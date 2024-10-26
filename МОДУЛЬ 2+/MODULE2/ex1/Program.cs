using System;

class Person
{
    // Поля для хранения информации о человеке
    private string name;
    private int age;
    private string address;


    // Метод для установки имени
    public void SetName(string name)
    {
        this.name = name;
    }

    // Метод для получения имени
    public string GetName()
    {
        return name;
    }

    // Метод для установки возраста
    public void SetAge(int age)
    {
        this.age = age;
    }

    // Метод для получения возраста
    public int GetAge()
    {
        return age;
    }

    // Метод для установки адреса
    public void SetAddress(string address)
    {
        this.address = address;
    }

    // Метод для получения адреса
    public string GetAddress()
    {
        return address;
    }
}

class Program
{
    static void Main()
    {
        // Создание объекта класса Person
        Person person = new Person();

        // Ввод имени
        Console.Write("Введите имя: ");
        string name = Console.ReadLine();
        person.SetName(name);

        // Ввод возраста
        Console.Write("Введите возраст: ");
        int age = int.Parse(Console.ReadLine());
        person.SetAge(age);

        // Ввод адреса
        Console.Write("Введите адрес: ");
        string address = Console.ReadLine();
        person.SetAddress(address);

        // Вывод информации о человеке
        Console.WriteLine($"\nИмя: {person.GetName()}, Возраст: {person.GetAge()}, Адрес: {person.GetAddress()}");
    }
}
