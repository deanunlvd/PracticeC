using System;

class Program
{
    // Метод для проверки, является ли число простым
    static bool IsPrime(int number)
    {
        // Отрицательные числа, 0 и 1 не являются простыми
        if (number <= 1)
            return false;

        // Проверка делимости числа на все числа от 2 до корня из number
        for (int i = 2; i * i <= number; i++)
        {
            if (number % i == 0)
                return false; // Если есть делитель, число не простое
        }

        return true; // Если делителей нет, число простое
    }

    static void Main()
    {
        Console.Write("Введите число: ");

        // Чтение числа от пользователя
        if (int.TryParse(Console.ReadLine(), out int number))
        {
            // Проверка, является ли число простым
            if (IsPrime(number))
            {
                Console.WriteLine($"Число {number} является простым.");
            }
            else
            {
                Console.WriteLine($"Число {number} не является простым.");
            }
        }
        else
        {
            Console.WriteLine("Некорректный ввод! Введите целое число.");
        }
    }
}
