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
using LabLogic;
using MathNet.Numerics;
using MathNet.Numerics.Interpolation;
using MathNet.Numerics.LinearAlgebra.Complex;
using MathNet.Numerics.LinearAlgebra;
using MathNet;

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
            MessageBox.Show("Дорабатывается...");
        }

        private void DrawGraphic(object sender, RoutedEventArgs e)
        {
            Program Logic = new Program();

            var plotModel = CreatePlotModel();
            //var lineSeries = CreateLineSeries();

            // Add the line series to the plot model
            //plotModel.Series.Add(lineSeries);

            // Add the axes to the plot model
            plotModel.Axes.Add(CreateLinearAxis("Time(Sec)", AxisPosition.Left));
            plotModel.Axes.Add(CreateLinearAxis("Runs", AxisPosition.Bottom));

            int firstParam = int.Parse(FirstParam.Text);
            int secondParam = int.Parse(SecondParam.Text);

            int function;
            if(int.TryParse(Function.Text, out function))
            {
                if (function == 0 || function > 12)
                {
                    MessageBox.Show("Такого алгоритма нет! Выберите от 1 до 12.");
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
                            double x = 1.5; // Replace this with the appropriate value
                            double y = Logic.StopWatchFunc(function, vector, x);
                            lineSeries.Points.Add(new DataPoint(i, y));
                        }
                        plotModel.Series.Add(lineSeries);
                    }
                }
            }
            else
            {
                MessageBox.Show("Введите число! Выберите от 1 до 12.");
            }
            // Assign the plot model to the plot view
            plotView.Model = plotModel;
        }

        private OxyColor GetColor(int index)
        {
            switch (index % 8)
            {
                case 1:
                    return OxyColors.White;
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
            var lineSeries = new LineSeries
            {
                StrokeThickness = 2,
                MarkerSize = 4,
                MarkerStroke = OxyColors.White,
                CanTrackerInterpolatePoints = false
            };
            return lineSeries;
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
                $"\n6)BubbleSort(Пузырьком) сортировка" +
                $"\n7)QuickSort(Быстрая) сортировка" +
                $"\n8)OddEvenSort(Чет-нечет) сортировка" +
                $"\n9)CombSort(Расческой) сортировка" +
                $"\n10)MultiplyMatrix(Умножение) матриц" +
                $"\n11)SelectionSort(Выборкой) сортировка" +
                $"\n12)TimSort(Вставками + слиянием)  сортировка" +
                $"\nДиапазон - кол-во чисел для исследования функции" +
                $"\nКол-во запусков - сколько раз запуститься алгоритм" +
                $"\nАппроксимация дорабатывается" +
                $"\nСложность для работы с аппроксимацией" );
        }
    }
}