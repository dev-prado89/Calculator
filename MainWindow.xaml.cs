using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculadora_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double? Num1 = null;
        private double? Num2 = null;
        private double? Resultado = null;
        private string Operador = string.Empty;
        private string? resto = string.Empty;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (button != null);
                string value = button.Content.ToString();
                                    
            if (double.TryParse(value, out double number))
            {
                if (Resultado.HasValue)
                {
                    Display.Text = string.Empty;
                    Resultado = null;
                }                   

                Display.Text += value;
            }
            else
            {                
                if (value == "=")
                {                    
                    if (Num1.HasValue && double.TryParse(Display.Text, out double result))
                    {
                        Num2 = result;
                        Resultado = Operacion(Num1.Value, Num2.Value, Operador);
                        Display.Text = Resultado.ToString();
                        resto = CalcAnterior(Num1.Value, Num2.Value, Operador, (double)Resultado);
                        ImpHistorial(resto);

                        Num1 = Resultado;
                        Num2 = null;
                        Operador = string.Empty;
                    }
                }
                else
                {                    
                    if (value == ",")
                    {
                        if (Num1 == null)
                            Display.Text = "0" + value;
                        else
                            Display.Text += value;
                    }                    
                    else if (double.TryParse(Display.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double num))
                    {
                        Num1 = num;
                        Operador = value;
                        Display.Text = string.Empty;
                    }
                    else if (double.TryParse(Display.Text, out double lastResult))
                    {                        
                        Num1 = lastResult;
                        Operador = value;
                                                
                        Display.Text = string.Empty;
                    }
                }
            }
        }

        private double Operacion(double num1, double num2, string operation)
        {
            switch (operation)
            {
                case "+":
                    return num1 + num2;
                case "-":
                    return num1 - num2;
                case "*":
                    return num1 * num2;
                case "/":
                    if (num2 != 0)
                        return num1 / num2;
                    else
                    {
                        MessageBox.Show("Error: División por cero.");
                        resto = CalcAnterior(num1, num2, Operador, 0);
                        ImpHistorial(resto);
                        return 0;
                    }
                default:
                    return 0;
            }
        }

        private void ImpHistorial(string guardado)
        {
            if (!string.IsNullOrEmpty(guardado))
            {
                TextBlock Previos = new TextBlock();
                Previos.TextAlignment = TextAlignment.Right;
                Previos.Text = guardado;
                Anterior.Children.Add(Previos);
            }
        }

        private string CalcAnterior(double num1, double num2, string operation, double result)
        {
            return $"{num1} {operation} {num2} = {result}";
        }
    }
}