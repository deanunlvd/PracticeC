using System;
using System.Windows;

namespace ex_2
{
    public partial class MainWindow : Window
    {
        private Notification notification;
        private NotificationHandler handler;
        public MainWindow()
        {
            InitializeComponent();
            // Создаем экземпляры классов
            notification = new Notification();
            handler = new NotificationHandler();

            // Регистрируем обработчики событий
            notification.OnMessageNotification += handler.HandleMessageNotification;
            notification.OnCallNotification += handler.HandleCallNotification;
            notification.OnEmailNotification += handler.HandleEmailNotification;
        }
        // Обработчик для кнопки "Отправить сообщение"
        private void OnSendMessage(object sender, RoutedEventArgs e)
        {
            string message = MessageTextBox.Text;
            notification.SendMessage(message);
        }

        // Обработчик для кнопки "Совершить звонок"
        private void OnMakeCall(object sender, RoutedEventArgs e)
        {
            string phoneNumber = "+375292166405";
            notification.MakeCall(phoneNumber);
        }

        // Обработчик для кнопки "Отправить email"
        private void OnSendEmail(object sender, RoutedEventArgs e)
        {
            string email = "kuinkevin7@gmail.com";
            notification.SendEmail(email);
        }
    }

    // Класс "Уведомление"
    public class Notification
    {
        public event Action<string> OnMessageNotification;
        public event Action<string> OnCallNotification;
        public event Action<string> OnEmailNotification;

        public void SendMessage(string message)
        {
            OnMessageNotification?.Invoke(message);
        }

        public void MakeCall(string phoneNumber)
        {
            OnCallNotification?.Invoke(phoneNumber);
        }

        public void SendEmail(string email)
        {
            OnEmailNotification?.Invoke(email);
        }
    }

    // Класс с обработчиками уведомлений
    public class NotificationHandler
    {
        public void HandleMessageNotification(string message)
        {
            MessageBox.Show($"Отправлено сообщение: {message}");
        }

        public void HandleCallNotification(string phoneNumber)
        {
            MessageBox.Show($"Совершен звонок на номер: {phoneNumber}");
        }

        public void HandleEmailNotification(string email)
        {
            MessageBox.Show($"Отправлено электронное письмо: {email}");
        }
    }
}
