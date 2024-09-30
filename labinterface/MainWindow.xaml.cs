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

namespace labinterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DrawApproximation(object sender, RoutedEventArgs e)
        {
            var plotModel = plotView.Model as PlotModel;
            var lineSeries = CreateLineSeries();

            // Заглушка для аппроксимации
            for (int i = 0; i <= 40; i++)
            {
                double x = i;
                double y = i; // Replace this with your approximating function
                lineSeries.Points.Add(new DataPoint(x, y));
            }

            // Add the approximating line series to the plot model
            plotModel.Series.Add(lineSeries);
        }

        private void DrawGraphic(object sender, RoutedEventArgs e)
        {
            Program Logic = new Program();

            var plotModel = CreatePlotModel();
            var lineSeries = CreateLineSeries();

            // Add the line series to the plot model
            plotModel.Series.Add(lineSeries);

            // Add the axes to the plot model
            plotModel.Axes.Add(CreateLinearAxis("Time", AxisPosition.Left));
            plotModel.Axes.Add(CreateLinearAxis("Sum", AxisPosition.Bottom));
            
            // Assign the plot model to the plot view
            plotView.Model = plotModel;
            int[] aaa = new int[2];
            //int aboba = Program.BubbleSort(aaa);
            // plotModel.Series.Add = 
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
                Color = OxyColors.Black
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
                IsAxisVisible = true
            };
        }
    }
}