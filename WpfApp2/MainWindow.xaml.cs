using System;
using System.Windows;
using MyApplication.IntegrationMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyApplication
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Считываем входные данные
                double upperLimit = double.Parse(tbUpperLimit.Text.Replace(',', '.'));
                double lowerLimit = double.Parse(tbLowerLimit.Text.Replace(',', '.'));
                int partitions = int.Parse(tbCount.Text);

                if (partitions <= 0)
                {
                    MessageBox.Show("Количество разбиений должно быть положительным целым числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Функция задана в Function.SampleFunction
                Func<double, double> function = Function.SampleFunction;

                // Проверка области определения: для ln(11x) требуется x > 0
                if (lowerLimit <= 0 || upperLimit <= 0)
                {
                    MessageBox.Show("Область определения функции требует x > 0 (логарифм). Укажите положительные пределы.", "Ошибка области определения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Если нижний предел > верхнего — поменяем местами и учтем знак
                bool swapped = false;
                if (lowerLimit > upperLimit)
                {
                    double tmp = lowerLimit;
                    lowerLimit = upperLimit;
                    upperLimit = tmp;
                    swapped = true;
                }

                // Выбор метода
                var selectedItem = (cmbMethod.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Tag?.ToString();
                IIntegrationMethod method;
                switch (selectedItem)
                {
                    case "MidpointRectangles":
                        method = new MidpointRectangles();
                        break;
                    case "Trapezoidal":
                        method = new Trapezoidal();
                        break;
                    default:
                        MessageBox.Show("Выберите метод интегрирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                }

                double result = method.Integrate(function, lowerLimit, upperLimit, partitions);
                if (swapped) result = -result; // учёт смены пределов

                tbAnswer.Text = result.ToString("F8");
            }
            catch (FormatException)
            {
                MessageBox.Show("Ошибка формата входных данных. Убедитесь, что в полях введены числа.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неожиданная ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}