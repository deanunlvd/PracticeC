using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Contacts
{
    public partial class MainWindow : Window
    {
        // Список для хранения контактов
        private List<Contact> contacts;
        public MainWindow()
        {
            InitializeComponent();
            contacts = new List<Contact>(); // Инициализация списка контактов
            lstContacts.ItemsSource = contacts; // Привязка списка контактов к ListBox
        }
        // Метод для добавления контакта
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtPhone.Text))
            {
                // Создание нового контакта
                Contact newContact = new Contact
                {
                    Name = txtName.Text,
                    Phone = txtPhone.Text,
                    Email = txtEmail.Text
                };
                contacts.Add(newContact); // Добавление контакта в список
                RefreshContactsList(); // Обновление отображения контактов
                ClearTextBoxes(); // Очистка текстовых полей
            }
            else
            {
                MessageBox.Show("Имя и телефон не могут быть пустыми.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Метод для редактирования выбранного контакта
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lstContacts.SelectedItem != null)
            {
                Contact selectedContact = (Contact)lstContacts.SelectedItem;
                selectedContact.Name = txtName.Text;
                selectedContact.Phone = txtPhone.Text;
                selectedContact.Email = txtEmail.Text;
                RefreshContactsList(); // Обновление списка
                ClearTextBoxes(); // Очистка полей
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите контакт для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Метод для удаления выбранного контакта
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstContacts.SelectedItem != null)
            {
                Contact selectedContact = (Contact)lstContacts.SelectedItem;
                contacts.Remove(selectedContact); // Удаление контакта
                RefreshContactsList(); // Обновление списка
                ClearTextBoxes(); // Очистка полей
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите контакт для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Метод для поиска контактов по имени
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = txtSearch.Text.ToLower();
            var filteredContacts = contacts.Where(c => c.Name.ToLower().Contains(searchQuery)).ToList();
            lstContacts.ItemsSource = filteredContacts; // Обновление источника данных ListBox
        }

        // Метод для обновления списка контактов в ListBox
        private void RefreshContactsList()
        {
            lstContacts.ItemsSource = null;
            lstContacts.ItemsSource = contacts;
        }

        // Метод для очистки текстовых полей
        private void ClearTextBoxes()
        {
            txtName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
        }

        private void lstContacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверяем, есть ли выбранный элемент
            if (lstContacts.SelectedItem is Contact selectedContact)
            {
                // Заполняем текстовые поля значениями выбранного контакта
                txtName.Text = selectedContact.Name;
                txtPhone.Text = selectedContact.Phone;
                txtEmail.Text = selectedContact.Email;
            }
        }


        // Класс Contact для представления контакта
        public class Contact
        {
            public string Name { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
        }
    }
}   