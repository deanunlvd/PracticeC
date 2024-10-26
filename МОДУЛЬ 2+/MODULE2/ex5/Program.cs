using System;

// Класс для датчика температуры
class TemperatureSensor
{
    private double temperature;

    // Событие, которое будет вызываться при изменении температуры
    public event Action<double> TemperatureChanged;

    // Свойство для получения текущей температуры
    public double Temperature
    {
        get => temperature;
        set
        {
            if (temperature != value) // Проверка на изменение температуры
            {
                temperature = value;
                OnTemperatureChanged(temperature); // Вызов события
            }
        }
    }

    // Метод для вызова события
    protected virtual void OnTemperatureChanged(double newTemperature)
    {
        TemperatureChanged?.Invoke(newTemperature);
    }
}

// Класс для термостата
class Thermostat
{
    private const double ThresholdTemperature = 22.0; // Пороговая температура
    private bool heatingOn;

    // Метод, который подписывается на событие TemperatureChanged
    public void Subscribe(TemperatureSensor sensor)
    {
        sensor.TemperatureChanged += HandleTemperatureChanged;
    }

    // Обработчик события TemperatureChanged
    private void HandleTemperatureChanged(double newTemperature)
    {
        Console.WriteLine($"Измеренная температура: {newTemperature}°C");

        // Логика управления отоплением
        if (newTemperature < ThresholdTemperature)
        {
            if (!heatingOn)
            {
                heatingOn = true;
                Console.WriteLine("Включаем отопление.");
            }
        }
        else
        {
            if (heatingOn)
            {
                heatingOn = false;
                Console.WriteLine("Выключаем отопление.");
            }
        }
    }
}

class Program
{
    static void Main()
    {
        // Создаем экземпляры классов
        TemperatureSensor sensor = new TemperatureSensor();
        Thermostat thermostat = new Thermostat();

        // Подписываем термостат на событие изменения температуры
        thermostat.Subscribe(sensor);

        // Изменяем температуру для проверки событий
        sensor.Temperature = 20.0; // Включает отопление
        sensor.Temperature = 23.0; // Выключает отопление
        sensor.Temperature = 21.0; // Никаких изменений
        sensor.Temperature = 22.0; // Включает отопление
    }
}
