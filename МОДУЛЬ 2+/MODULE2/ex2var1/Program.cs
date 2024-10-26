using System;

struct Train
{
    public string Destination; // Название пункта назначения
    public int TrainNumber; // Номер поезда
    public DateTime DepartureTime; // Время отправления

    // Метод для вывода информации о поезде
    public void DisplayInfo()
    {
        Console.WriteLine($"Поезд №{TrainNumber}: Пункт назначения - {Destination}, Время отправления - {DepartureTime}");
    }
}

class Program
{
    static void Main()
    {
        Train[] trains = new Train[5];

        // Ввод данных о поездах
        for (int i = 0; i < trains.Length; i++)
        {
            Console.WriteLine($"Введите информацию о поезде {i + 1}:");
            Console.Write("Пункт назначения: ");
            trains[i].Destination = Console.ReadLine();

            Console.Write("Номер поезда: ");
            trains[i].TrainNumber = int.Parse(Console.ReadLine());

            Console.Write("Время отправления (в формате ГГГГ-ММ-ДД ЧЧ:ММ): ");
            trains[i].DepartureTime = DateTime.Parse(Console.ReadLine());
            Console.WriteLine();
        }

        // Сортировка по номерам поездов
        Array.Sort(trains, (t1, t2) => t1.TrainNumber.CompareTo(t2.TrainNumber));

        Console.WriteLine("Поезда отсортированы по номерам:");
        foreach (var train in trains)
        {
            train.DisplayInfo();
        }

        // Поиск поезда по номеру
        Console.Write("Введите номер поезда для поиска информации: ");
        int searchNumber = int.Parse(Console.ReadLine());
        Train? foundTrain = null;

        foreach (var train in trains)
        {
            if (train.TrainNumber == searchNumber)
            {
                foundTrain = train;
                break;
            }
        }

        if (foundTrain.HasValue)
        {
            Console.WriteLine("Информация о найденном поезде:");
            foundTrain.Value.DisplayInfo();
        }
        else
        {
            Console.WriteLine("Поезд с таким номером не найден.");
        }

        // Сортировка по пункту назначения
        Array.Sort(trains, (t1, t2) =>
        {
            int destinationComparison = t1.Destination.CompareTo(t2.Destination);
            if (destinationComparison == 0)
            {
                return t1.DepartureTime.CompareTo(t2.DepartureTime); // Сортировка по времени отправления
            }
            return destinationComparison;
        });

        Console.WriteLine("Поезда отсортированы по пунктам назначения:");
        foreach (var train in trains)
        {
            train.DisplayInfo();
        }
    }
}
