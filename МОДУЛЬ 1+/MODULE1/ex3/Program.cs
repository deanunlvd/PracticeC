using System;

class Program
{
    static void Main()
    {
        // Запрос строки у пользователя
        Console.Write("Введите строку: ");
        string input = Console.ReadLine();

        // Преобразование строки в обратный порядок
        char[] charArray = input.ToCharArray();
        Array.Reverse(charArray);
        string reversedString = new string(charArray);

        // Вывод результата
        Console.WriteLine($"Строка в обратном порядке: {reversedString}");
    }
}
