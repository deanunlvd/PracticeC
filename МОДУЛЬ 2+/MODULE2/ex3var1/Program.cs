using System;

abstract class Shape
{
    // Абстрактные методы для вычисления площади и периметра
    public abstract double Area();
    public abstract double Perimeter();
}

class Circle : Shape
{
    public double Radius { get; set; }

    public Circle(double radius)
    {
        Radius = radius;
    }

    public override double Area()
    {
        return Math.PI * Radius * Radius; // Площадь круга
    }

    public override double Perimeter()
    {
        return 2 * Math.PI * Radius; // Периметр круга
    }
}

class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public override double Area()
    {
        return Width * Height; // Площадь прямоугольника
    }

    public override double Perimeter()
    {
        return 2 * (Width + Height); // Периметр прямоугольника
    }
}

class Triangle : Shape
{
    public double SideA { get; set; }
    public double SideB { get; set; }
    public double SideC { get; set; }

    public Triangle(double sideA, double sideB, double sideC)
    {
        SideA = sideA;
        SideB = sideB;
        SideC = sideC;
    }

    public override double Area()
    {
        // Формула Герона для вычисления площади треугольника
        double semiPerimeter = Perimeter() / 2;
        return Math.Sqrt(semiPerimeter * (semiPerimeter - SideA) * (semiPerimeter - SideB) * (semiPerimeter - SideC));
    }

    public override double Perimeter()
    {
        return SideA + SideB + SideC; // Периметр треугольника
    }
}

class Program
{
    static void Main()
    {
        // Создание объектов фигур
        Shape circle = new Circle(5);
        Shape rectangle = new Rectangle(4, 6);
        Shape triangle = new Triangle(3, 4, 5);

        // Вывод информации о фигурах
        Console.WriteLine($"Круг: Площадь = {circle.Area():F2}, Периметр = {circle.Perimeter():F2}");
        Console.WriteLine($"Прямоугольник: Площадь = {rectangle.Area():F2}, Периметр = {rectangle.Perimeter():F2}");
        Console.WriteLine($"Треугольник: Площадь = {triangle.Area():F2}, Периметр = {triangle.Perimeter():F2}");
    }
}
