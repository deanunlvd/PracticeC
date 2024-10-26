using System;

class Program
{
    // Метод для вычисления факториала
    static long Factorial(int number)
    {
        if (number <= 1)
            return 1;
        return number * Factorial(number - 1);
    }

    static void Main()
    {
        Console.Write("Введите число для вычисления факториала: ");

        // Чтение числа от пользователя
        if (int.TryParse(Console.ReadLine(), out int number))
        {
            // Проверка на отрицательное число
            if (number < 0)
            {
                Console.WriteLine("Факториал отрицательного числа не определён.");
            }
            else
            {
                // Вызов метода для вычисления факториала
                long result = Factorial(number);
                Console.WriteLine($"Факториал числа {number} равен {result}");
            }
        }
        else
        {
            Console.WriteLine("Некорректный ввод! Введите целое число.");
        }
    }
}
