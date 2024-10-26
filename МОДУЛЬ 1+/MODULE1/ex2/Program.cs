using System;

class Program
{
    static void Main()
    {
        // Запрос первого числа
        Console.Write("Введите первое целое число: ");
        if (int.TryParse(Console.ReadLine(), out int firstNumber))
        {
            // Запрос второго числа
            Console.Write("Введите второе целое число: ");
            if (int.TryParse(Console.ReadLine(), out int secondNumber))
            {
                // Вычисление и вывод суммы
                int sum = firstNumber + secondNumber;
                Console.WriteLine($"Сумма чисел {firstNumber} и {secondNumber} равна {sum}");
            }
            else
            {
                Console.WriteLine("Некорректный ввод! Введите целое число.");
            }
        }
        else
        {
            Console.WriteLine("Некорректный ввод! Введите целое число.");
        }
    }
}
