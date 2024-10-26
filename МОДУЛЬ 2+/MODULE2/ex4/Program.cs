using System;

interface IDrawable
{
    // Метод для рисования объекта
    void Draw();
}

class Circle : IDrawable
{
    private double radius;

    // Конструктор для круга
    public Circle(double radius)
    {
        this.radius = radius;
    }

    // Реализация метода Draw для круга
    public void Draw()
    {
        Console.WriteLine($"Рисуем круг с радиусом {radius}");
    }
}

class Rectangle : IDrawable
{
    private double width;
    private double height;

    // Конструктор для прямоугольника
    public Rectangle(double width, double height)
    {
        this.width = width;
        this.height = height;
    }

    // Реализация метода Draw для прямоугольника
    public void Draw()
    {
        Console.WriteLine($"Рисуем прямоугольник с шириной {width} и высотой {height}");
    }
}

class Triangle : IDrawable
{
    private double baseLength;
    private double height;

    // Конструктор для треугольника
    public Triangle(double baseLength, double height)
    {
        this.baseLength = baseLength;
        this.height = height;
    }

    // Реализация метода Draw для треугольника
    public void Draw()
    {
        Console.WriteLine($"Рисуем треугольник с основанием {baseLength} и высотой {height}");
    }
}

class Program
{
    static void Main()
    {
        // Создание массива объектов, реализующих интерфейс IDrawable
        IDrawable[] shapes = new IDrawable[]
        {
            new Circle(5.0),
            new Rectangle(4.0, 6.0),
            new Triangle(3.0, 4.0)
        };

        // Вызов метода Draw для каждого объекта в массиве
        foreach (IDrawable shape in shapes)
        {
            shape.Draw();
        }
    }
}
