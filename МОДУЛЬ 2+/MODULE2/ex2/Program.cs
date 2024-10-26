using System;

abstract class Shape
{
    // Метод для вычисления площади
    public abstract double Area();

    // Метод для вычисления периметра
    public abstract double Perimeter();
}

class Circle : Shape
{
    private double radius;

    // Конструктор для круга
    public Circle(double radius)
    {
        this.radius = radius;
    }

    // Переопределение метода Area для круга
    public override double Area()
    {
        return Math.PI * radius * radius;
    }

    // Переопределение метода Perimeter для круга
    public override double Perimeter()
    {
        return 2 * Math.PI * radius;
    }
}

class Rectangle : Shape
{
    private double width;
    private double height;

    // Конструктор для прямоугольника
    public Rectangle(double width, double height)
    {
        this.width = width;
        this.height = height;
    }

    // Переопределение метода Area для прямоугольника
    public override double Area()
    {
        return width * height;
    }

    // Переопределение метода Perimeter для прямоугольника
    public override double Perimeter()
    {
        return 2 * (width + height);
    }
}

class Program
{
    static void Main()
    {
        // Создание объекта класса Circle
        Circle circle = new Circle(5.0);
        Console.WriteLine($"Круг: Площадь = {circle.Area()}, Периметр = {circle.Perimeter()}");

        // Создание объекта класса Rectangle
        Rectangle rectangle = new Rectangle(4.0, 6.0);
        Console.WriteLine($"Прямоугольник: Площадь = {rectangle.Area()}, Периметр = {rectangle.Perimeter()}");
    }
}
