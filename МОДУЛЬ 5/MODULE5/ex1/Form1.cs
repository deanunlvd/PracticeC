using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ex1
{
    public partial class Form1 : Form
    {
        private string selectedShape = ""; // Выбранная фигура
        private Point startPoint; // Начальная точка для рисования
        private bool isDrawing = false; // Флаг, который указывает, что идет процесс рисования
        private List<(string Shape, Point Start, Point End)> shapes = new List<(string, Point, Point)>(); // Список для хранения фигур
        public Form1()
        {
            InitializeComponent();
           
        }
        // Обработчики для кнопок
        private void btnLine_Click(object sender, EventArgs e)
        {
            selectedShape = "Линия"; // Устанавливаем фигуру "Линия"
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            selectedShape = "Круг"; // Устанавливаем фигуру "Круг"
        }

        private void btnSquare_Click(object sender, EventArgs e)
        {
            selectedShape = "Квадрат"; // Устанавливаем фигуру "Квадрат"
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            shapes.Clear(); // Очищаем список фигур
            panelCanvas.Invalidate(); // Перерисовываем панель, чтобы удалить все фигуры
        }

        // Начало рисования, когда нажимается мышь
        private void panelCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedShape)) // Проверяем, выбрана ли фигура
            {
                startPoint = e.Location; // Запоминаем начальную точку
                isDrawing = true; // Устанавливаем флаг начала рисования
            }
        }

        // Окончание рисования, когда отпускается мышь
        private void panelCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                isDrawing = false; // Завершаем рисование
                Point endPoint = e.Location; // Конечная точка

                // Сохраняем информацию о нарисованной фигуре
                shapes.Add((selectedShape, startPoint, endPoint));

                // Перерисовываем панель
                panelCanvas.Invalidate(); // Вызывает событие  для перерисовки всех фигур
            }
        }

        // Обработчик события для рисования всех фигур
        private void panelCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Проходим по списку фигур и рисуем каждую
            foreach (var shape in shapes)
            {
                switch (shape.Shape)
                {
                    case "Линия":
                        g.DrawLine(Pens.Black, shape.Start, shape.End); // Рисуем линию
                        break;
                    case "Круг":
                        DrawCircle(g, shape.Start, shape.End); // Рисуем круг
                        break;
                    case "Квадрат":
                        DrawSquare(g, shape.Start, shape.End); // Рисуем квадрат
                        break;
                }
            }
        }

        // Метод для рисования круга
        private void DrawCircle(Graphics g, Point start, Point end)
        {
            // Вычисляем радиус и центр
            int radius = Math.Min(Math.Abs(end.X - start.X), Math.Abs(end.Y - start.Y)) / 2;
            int centerX = (start.X + end.X) / 2 - radius;
            int centerY = (start.Y + end.Y) / 2 - radius;
            g.DrawEllipse(Pens.Black, centerX, centerY, radius * 2, radius * 2); // Рисуем круг
        }

        // Метод для рисования квадрата
        private void DrawSquare(Graphics g, Point start, Point end)
        {
            // Вычисляем сторону квадрата
            int side = Math.Min(Math.Abs(end.X - start.X), Math.Abs(end.Y - start.Y));
            int topX = start.X;
            int topY = start.Y;
            g.DrawRectangle(Pens.Black, topX, topY, side, side); // Рисуем квадрат
        }
    }
}