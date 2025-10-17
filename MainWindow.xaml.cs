using System;
using System.Windows;
using MyApplication.IntegrationMethods;

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
                double upperLimit = double.Parse(tbUpperLimit.Text.Replace(',', '.'));
                double lowerLimit = double.Parse(tbLowerLimit.Text.Replace(',', '.'));
                int partitions = int.Parse(tbCount.Text);

                if (partitions <= 0)
                {
                    MessageBox.Show("Количество разбиений должно быть положительным.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Func<double, double> function = Function.SampleFunction;

                if (lowerLimit <= 0 || upperLimit <= 0)
                {
                    MessageBox.Show("Область определения функции требует x > 0 (логарифм).", "Ошибка области определения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                bool swapped = false;
                if (lowerLimit > upperLimit)
                {
                    double tmp = lowerLimit;
                    lowerLimit = upperLimit;
                    upperLimit = tmp;
                    swapped = true;
                }

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
                if (swapped) result = -result;

                tbAnswer.Text = result.ToString("F3");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}
