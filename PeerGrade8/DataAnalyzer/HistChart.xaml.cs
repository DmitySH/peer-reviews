using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;

namespace DataAnalyzer
{
    /// <summary>
    /// Interaction logic for HistChart.xaml
    /// </summary>
    public partial class HistChart : Window
    {
        public List<int> newQuantity { get; set; }
        public SolidColorBrush Brush { get; set; } = new SolidColorBrush(Colors.Aqua);
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public string YLabel { get; set; }
        public ColumnSeries Cols { get; set; }

        /// <summary>
        /// Constructor for chart window.
        /// </summary>
        /// <param name="prev"> Previous window. </param>
        public HistChart(Window prev)
        {
            InitializeComponent();
            Histogram(prev as HistConfig);
        }

        /// <summary>
        /// Gives data to chart.
        /// </summary>
        /// <param name="prev"> Previous window. </param>
        void Histogram(HistConfig prev)
        {
            prev.Data.Sort();
            HashSet<double> values = new HashSet<double>(prev.Data);
            List<double> vals = new List<double>(values);
            List<int> quantity = new List<int>();

            foreach (var value in values)
            {
                quantity.Add(prev.Data.Count(x => Math.Abs(x - value) < 0.01));
            }

            newQuantity = new List<int>(10);
            var newLabels = new List<string>();

            int k = int.Parse(prev.UpDown.Value.ToString());
            ChangeBarsWidth(quantity, vals, k, newLabels, newQuantity);
            Labels = Array.ConvertAll(newLabels.ToArray(), (el) => el.ToString(CultureInfo.InvariantCulture));

            Cols = new ColumnSeries
            {
                Title = $"Quantitative{Environment.NewLine}analysis",
                Values = new ChartValues<int>(newQuantity),
                Fill = Brush
            };
            SeriesCollection = new SeriesCollection { Cols };
            YLabel = prev.YLabel;
            DataContext = this;
        }

        /// <summary>
        /// Changes bars' width.
        /// </summary>
        /// <param name="k"> Counts of parts to be. </param>
        /// <param name="quantity"> List with quantity of values. </param>
        /// <param name="newLabels"> List with final labels. </param>
        /// <param name="vals"> List with values. </param>
        /// <param name="newQuantity"> List with final quantity of values. </param>
        private static void ChangeBarsWidth(List<int> quantity, List<double> vals, int k,
            List<string> newLabels, List<int> newQuantity)
        {
            int sum = 0;
            for (int i = 0; i < quantity.Count; i++)
            {
                sum += quantity[i];
                if (i == quantity.Count - 1 && vals.Count % k != 0)
                {
                    newLabels.Add($"{vals[vals.Count - vals.Count % k]}-{vals[i]}");
                    newQuantity.Add(sum);
                    break;
                }

                if ((i + 1) % k == 0)
                {
                    newLabels.Add($"{vals[i + 1 - k]}-{vals[i]}");
                    newQuantity.Add(sum);
                    sum = 0;
                }
            }
        }

        /// <summary>
        /// Saves chart.
        /// </summary>
        /// <param name="visual"> View of chart. </param>
        /// <param name="fileName"> Path. </param>
        public static void SaveToPng(FrameworkElement visual, string fileName)
        {
            try
            {
                var encoder = new PngBitmapEncoder();
                EncodeVisual(visual, fileName, encoder);
            }
            catch (Exception)
            {
                MessageBox.Show("Can't save chart.");
            }
        }

        /// <summary>
        /// Encodes. Taken from: https://question-it.com/questions/341929/c-diagrammy-k-izobrazheniju-s-livecharts
        /// </summary>
        /// <param name="visual"> Chart's view. </param>
        /// <param name="fileName"> Path. </param>
        /// <param name="encoder"> Encoder. </param>
        private static void EncodeVisual(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            var bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            var frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);
            using var stream = File.Create(fileName);
            encoder.Save(stream);
        }

        /// <summary>
        /// Saves chart.
        /// </summary>
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e) => SaveToPng(HistogramView, "Hist.png");

        /// <summary>
        /// Changes color of series.
        /// </summary>
        private void ColorPicker_OnSelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Brush = new SolidColorBrush(ColorPicker.SelectedColor.Value);
            SeriesCollection.RemoveAt(0);
            Cols = new ColumnSeries
            {
                Title = $"Quantitative{Environment.NewLine}analysis",
                Values = new ChartValues<int>(newQuantity),
                Fill = Brush
            };
            SeriesCollection.Add(Cols);
        }
    }
}
