using System;
using System.Windows.Forms;

namespace ex5
{
    public partial class Form1 : Form
    {
        private double result = 0;
        private string operation = "";
        private bool isOperationPerformed = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            if ((textBoxResult.Text == "0") || (isOperationPerformed))
                textBoxResult.Clear();

            isOperationPerformed = false;
            Button button = (Button)sender;
            textBoxResult.Text += button.Text;
        }

        private void operator_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            operation = button.Text;
            result = double.Parse(textBoxResult.Text);
            isOperationPerformed = true;
        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            switch (operation)
            {
                case "+":
                    textBoxResult.Text = (result + double.Parse(textBoxResult.Text)).ToString();
                    break;
                case "-":
                    textBoxResult.Text = (result - double.Parse(textBoxResult.Text)).ToString();
                    break;
                case "*":
                    textBoxResult.Text = (result * double.Parse(textBoxResult.Text)).ToString();
                    break;
                case "/":
                    if (double.Parse(textBoxResult.Text) != 0)
                        textBoxResult.Text = (result / double.Parse(textBoxResult.Text)).ToString();
                    else
                        MessageBox.Show("На ноль делить нельзя!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    break;
            }
            result = double.Parse(textBoxResult.Text);
            operation = "";
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxResult.Text = "0";
            result = 0;
            operation = "";
        }
    }
}
