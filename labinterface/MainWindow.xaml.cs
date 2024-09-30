using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Wpf;
using OxyPlot.Series;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro;
using MahApps;
using MahApps.Metro.Controls;
using LabLogic;
using ControlzEx.Standard;
using MathNet.Numerics;

namespace labinterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DrawApproximation(object sender, RoutedEventArgs e)
        {
            var plotModel = plotView.Model as PlotModel;
            var lineSeries = CreateLineSeries2();
           
            double[] elements = new double[3000];

            for (int i = 0; i < elements.Length; i++)
            {
                elements[i] = i + 1;
            }

            double[] data = new double[3000];
            var coefficients = Fit.Polynomial(elements, data, 3);

            for (int i = 0; i < data.Length; i++)
            {
                double value = coefficients[0] + coefficients[1] * (i + 1) + coefficients[2] * Math.Pow(i + 1, 2);
                lineSeries.Points.Add(new DataPoint(i + 1, value));
            }

            // Add the approximating line series to the plot model
            plotModel.Series.Add(lineSeries);
        }

        
        private void DrawGraphic(object sender, RoutedEventArgs e)
        {
            Program Logic = new Program();

            var plotModel = CreatePlotModel();
            //var lineSeries = CreateLineSeries();

            // Add the line series to the plot model
            //plotModel.Series.Add(lineSeries);

            // Add the axes to the plot model
            plotModel.Axes.Add(CreateLinearAxis("Time", AxisPosition.Left));
            plotModel.Axes.Add(CreateLinearAxis("Runs", AxisPosition.Bottom));

            int firstParam = int.Parse(FirstParam.Text);
            int secondParam = int.Parse(SecondParam.Text);

            int function;
            if(int.TryParse(Function.Text, out function))
            {
                if (function == 0 || function >= 9)
                {
                    MessageBox.Show("Такого алгоритма нет! Выберите от 1 до 8.");
                }
                else
                {
                    for(int j = 1; j <= secondParam; j++)
                    {
                        var lineSeries = CreateLineSeries();
                        lineSeries.Color = GetColor(j);

                        for (int i = (int)0.1; i <= firstParam; i++)
                        {
                            int[] vector = Enumerable.Repeat(i, i).ToArray();
                            double x = 3; // Replace this with the appropriate value
                            double y = Logic.StopWatchFunc(function, vector, x);
                            lineSeries.Points.Add(new DataPoint(i, y));
                        }
                        plotModel.Series.Add(lineSeries);
                    }
                }
            }
            else
            {
                MessageBox.Show("Введите число! Выберите от 1 до 8.");
            }
            // Assign the plot model to the plot view
            plotView.Model = plotModel;
        }

        private OxyColor GetColor(int index)
        {
            switch (index % 8)
            {
                case 1:
                    return OxyColors.Red;
                case 2:
                    return OxyColors.Green;
                case 3:
                    return OxyColors.Blue;
                case 4:
                    return OxyColors.Yellow;
                case 5:
                    return OxyColors.Cyan;
                case 6:
                    return OxyColors.Magenta;
                case 7:
                    return OxyColors.Orange;
                case 0:
                    return OxyColors.Purple;
                default:
                    return OxyColors.Black;
            }
        }

        private PlotModel CreatePlotModel()
        {
            return new PlotModel
            {
                PlotAreaBorderThickness = new OxyThickness(0.5),
                PlotMargins = new OxyThickness(30)
            };
        }

        private LineSeries CreateLineSeries()
        {
            return new LineSeries
            {
                StrokeThickness = 2,
                Color = OxyColors.White
            };
        }

        private LineSeries CreateLineSeries2()
        {
            return new LineSeries
            {
                StrokeThickness = 2,
                Color = OxyColors.Red
            };
        }

        private LinearAxis CreateLinearAxis(string title, AxisPosition position)
        {
            return new LinearAxis
            {
                Title = title,
                AxislineColor = OxyColors.Red,
                Maximum = 40,
                Minimum = -40,
                PositionAtZeroCrossing = true,
                Position = position,
                TickStyle = TickStyle.Crossing,
                IsAxisVisible = true,
            };
        }

        private void GetInformation(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Выбор функции:\n1)Постоянная функция" +
                $"\n2)Сумма элементов" +
                $"\n3)Произведение элементов" +
                $"\n4)Полином" +
                $"\n5)Метод Горнера" +
                $"\n6)Пузырьковая сортировка" +
                $"\n7)Быстрая сортировка" +
                $"\n8)TimSort сортировка" +
                $"\nДиапазон - кол-во чисел для исследования функции" +
                $"\nКол-во запусков - сколько раз запуститься алгоритм");
        }
    }
}