using System;
using System.Linq;

class Program
{
    static void Main()
    {
        // Инициализация генератора случайных чисел
        Random random = new Random();

        // Создание массива из 15 случайных чисел в диапазоне от -100 до 100
        int[] numbers = new int[15];
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = random.Next(-100, 101); // случайные числа от -100 до 100 включительно
        }

        // Вывод массива
        Console.WriteLine("Массив случайных чисел: ");
        Console.WriteLine(string.Join(", ", numbers));

        // Поиск положительных чисел
        var positiveNumbers = numbers.Where(n => n > 0);

        // Вычисление среднего значения положительных чисел
        if (positiveNumbers.Any())
        {
            double average = positiveNumbers.Average();
            Console.WriteLine($"Среднее значение положительных чисел: {average:F2}");
        }
        else
        {
            Console.WriteLine("В массиве нет положительных чисел.");
        }
    }
}
