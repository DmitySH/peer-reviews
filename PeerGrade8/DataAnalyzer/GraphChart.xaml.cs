using System;
using System.Collections.Generic;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using Point = System.Windows.Point;

namespace DataAnalyzer
{
    /// <summary>
    /// Interaction logic for GraphChart.xaml
    /// </summary>
    public partial class GraphChart : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public string YLabel { get; set; }
        public string XLabel { get; set; }

        /// <summary>
        /// Constructor for graphic chart.
        /// </summary>
        /// <param name="prev"> Previous window. </param>
        public GraphChart(Window prev)
        {
            InitializeComponent();

            Graphic(prev as GraphConfig);
        }

        /// <summary>
        /// Comparer for points.
        /// </summary>
        static int Comparer(Point p1, Point p2)
        {
            if (p1.X > p2.X)
            {
                return 1;
            }
            if (p1.X < p2.X)
            {
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// Gives data to chart.
        /// </summary>
        /// <param name="prev"> Previous window. </param>
        void Graphic(GraphConfig prev)
        {
            List<Point> data = new List<Point>(50);
            for (int i = 0; i < prev.XData.Count; i++)
            {
                data.Add(new Point(prev.XData[i], prev.YData[i]));
            }
            data.Sort(Comparer);

            Labels = Array.ConvertAll(data.ToArray(), (el) => el.X.ToString());
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = prev.YLabel,
                    Values = new ChartValues<double>(Array.ConvertAll(data.ToArray(), el => el.Y)),
                }
            };
            XLabel = prev.XLabel;
            YLabel = prev.YLabel;

            DataContext = this;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e) =>
            HistChart.SaveToPng(GraphicView, "Graph.png");
    }
}
