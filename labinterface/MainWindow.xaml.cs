using System.Linq;
using System.Windows;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using LabLogic;
using System.Windows.Controls;

namespace labinterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : System.Windows.Window
    {
        private PlotModel plotModel;

        public MainWindow()
        {
            InitializeComponent();
            plotModel = CreatePlotModel();
            plotModel.Axes.Add(CreateLinearAxis("Time(Sec)", AxisPosition.Left));
            plotModel.Axes.Add(CreateLinearAxis("Runs", AxisPosition.Bottom));
            plotView.Model = plotModel;
        }

        private void DrawApproximation(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Дорабатывается...");
        }

        private void ToggleAllExceptLastSeries(bool isVisible)
        {
            // Проверяем, что в plotModel есть хотя бы одна серия
            if (plotModel != null && plotModel.Series.Count > 1)
            {
                // Проходим по всем сериям, кроме последней
                for (int i = 0; i < plotModel.Series.Count - 1; i++)
                {
                    plotModel.Series[i].IsVisible = isVisible;
                }

                // Обновляем график
                plotModel.InvalidatePlot(true);
            }
        }

        private void Other_Checked(object sender, RoutedEventArgs e)
        {
            ToggleAllExceptLastSeries(true);
        }

        private void Other_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleAllExceptLastSeries(false);
        }

        private void Avarage_Checked(object sender, RoutedEventArgs e)
        {
            if (plotModel != null && plotModel.Series.Count > 0)
            {
                var lastSeries = plotModel.Series[plotModel.Series.Count - 1];
                lastSeries.IsVisible = true;
                plotModel.InvalidatePlot(true);
            }
        }

        private void Avarage_Unchecked(object sender, RoutedEventArgs e)
        {
            if (plotModel != null && plotModel.Series.Count > 0)
            {
                var lastSeries = plotModel.Series[plotModel.Series.Count - 1];
                lastSeries.IsVisible = false;
                plotModel.InvalidatePlot(true);
            }
        }

        private void DrawGraphic(object sender, RoutedEventArgs e)
        {
            IPowAlgorithm[] algorithms = new IPowAlgorithm[]
                {
                    new PowStandart(),
                    new QuickPow(),
                    new QuickPow1(),
                    new RecPow()
                };

            Program Logic = new Program();
            plotModel.Series.Clear();

            //plotModel = CreatePlotModel();
            //var lineSeries = CreateLineSeries();

            // Add the line series to the plot model
            //plotModel.Series.Add(lineSeries);

            // Add the axes to the plot model
            //plotModel.Axes.Add(CreateLinearAxis("Time(Sec)", AxisPosition.Left));
            //plotModel.Axes.Add(CreateLinearAxis("Runs", AxisPosition.Bottom));

            int firstParam = int.Parse(FirstParam.Text);
            int secondParam = int.Parse(SecondParam.Text);

            bool otherVisibility = (bool)Other.IsChecked;
            bool avarageVisibility = (bool)Avarage.IsChecked;

            ComboBoxItem selectedItem = (ComboBoxItem)Functions.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Выберете алгоритм");
            }
            else
            {
                string function = selectedItem.Content.ToString();
                for (int j = 1; j <= secondParam; j++)
                {
                    var lineSeries1 = CreateLineSeries();
                    lineSeries1.Color = GetColor(j, secondParam);

                    for (int i = (int)0.1; i <= firstParam; i++)
                    {
                        int[] vector = Enumerable.Repeat(i, i).ToArray();
                        double x = 1.5; // Replace this with the appropriate value
                        double y = Logic.StopWatchFunc(function, vector, x, i);
                        lineSeries1.Points.Add(new DataPoint(i, y));
                    }
                    if (function != "Постаянная функция")
                    {
                        for (int k = 1; k <= 20; k++)
                        {
                            for (int i = 1; i < lineSeries1.Points.Count; i++)
                            {
                                var line = lineSeries1.Points;
                                double first;
                                if (i == 1)
                                    first = 0;
                                else
                                    first = line[i - 2].Y;
                                double second = line[i - 1].Y;
                                double third = line[i].Y;
                                if ((first * 1.1 < second || second > third * 1.1))
                                {
                                    lineSeries1.Points[i - 1] = new DataPoint(line[i - 1].X, (first + third) / 2);
                                }
                            }
                        }
                    }
                    plotModel.Series.Add(lineSeries1);
                    if (!otherVisibility)
                        plotModel.Series[plotModel.Series.Count - 1].IsVisible = false;

                }
                var lineSeries2 = CreateLineSeries();
                lineSeries2.Color = OxyColors.Red;
                int c = 0;
                foreach (var series in plotModel.Series)
                {
                    c++;
                    var currentLineSeries = ((LineSeries)series).Points;
                    for (int i = 0; i < currentLineSeries.Count; i++)
                    {
                        if (i < lineSeries2.Points.Count )
                        {
                            DataPoint point1 = currentLineSeries[i];
                            DataPoint point2 = lineSeries2.Points[i];
                            lineSeries2.Points[i] = new DataPoint(point1.X, ((c - 1) * point2.Y + point1.Y) / c);
                        }
                        else
                        {
                            lineSeries2.Points.Add(new DataPoint(i, currentLineSeries[i].Y));
                        }
                    }
                }
                plotModel.Series.Add(lineSeries2);
                if (!avarageVisibility)
                    plotModel.Series[plotModel.Series.Count - 1].IsVisible = false;
                UpdatePlotAxes();
                plotModel.InvalidatePlot(true);
            }
        }

        private void UpdatePlotAxes()
        {
            var lastSeries = (LineSeries)plotModel.Series[plotModel.Series.Count - 1];
            DataPoint point = lastSeries.Points[lastSeries.Points.Count - 1];

            double minX = 0 - point.X / 10;
            double maxX = point.X + point.X / 10;
            double minY = 0 - point.Y / 10;
            double maxY = point.Y + point.Y / 10;

            plotModel.Axes.Clear(); // Очищаем существующие оси, если нужно

            // Создаем новые оси
            plotModel.Axes.Add(new LinearAxis {
                Title = "Runs",
                Position = AxisPosition.Bottom,
                Minimum = minX,
                Maximum = maxX,
                PositionAtZeroCrossing = true,
                TickStyle = TickStyle.Crossing,
                IsAxisVisible = true,
            });
            plotModel.Axes.Add(new LinearAxis {
                Title = "Time(Sec)",
                Position = AxisPosition.Left,
                Minimum = minY,
                Maximum = maxY,
                PositionAtZeroCrossing = true,
                TickStyle = TickStyle.Crossing,
                IsAxisVisible = true,
            });

            // Обновляем график
            plotModel.InvalidatePlot(true);
        }

        private OxyColor GetColor(int index, int count)
        {
            if (count <= 1 || index < 1 || index > count)
            {
                return OxyColors.Yellow;
            }

            OxyColor startColor = OxyColors.Yellow;
            OxyColor endColor = OxyColors.Green;

            double fraction = (double)(index - 1) / (count - 1);

            return InterpolateColor(startColor, endColor, fraction);
        }

        private OxyColor InterpolateColor(OxyColor startColor, OxyColor endColor, double fraction)
        {
            byte r = (byte)(startColor.R + (endColor.R - startColor.R) * fraction);
            byte g = (byte)(startColor.G + (endColor.G - startColor.G) * fraction);
            byte b = (byte)(startColor.B + (endColor.B - startColor.B) * fraction);

            return OxyColor.FromRgb(r, g, b);
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
            MessageBox.Show(
                $"\nДиапазон - кол-во чисел для исследования функции, для степенных функций является степенью" +
                $"\nКол-во запусков - сколько раз запуститься алгоритм"
                );
        }

        private void ThirdParam_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void FirstParam_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}