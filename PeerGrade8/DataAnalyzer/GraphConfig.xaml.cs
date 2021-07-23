using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows;

namespace DataAnalyzer
{
    /// <summary>
    /// Interaction logic for GraphConfig.xaml
    /// </summary>
    public partial class GraphConfig : Window
    { 
        public string XLabel { get; set; }
        public string YLabel { get; set; }
        public List<double> XData { get; set; }
        public List<double> YData { get; set; }

        private readonly MainWindow prev;
        private readonly List<string> headers = new();
        private readonly DataTable dt;

        /// <summary>
        /// Constructor for config window.
        /// </summary>
        /// <param name="prev"> Previous window. </param>
        public GraphConfig(MainWindow prev)
        {
            InitializeComponent();
            dt = prev.Dt;
            this.prev = prev;
            InitializeBoxes();
        }

        /// <summary>
        /// Initializes list boxes.
        /// </summary>
        private void InitializeBoxes()
        {
            foreach (var col in prev.CSVGrid.Columns)
            {
                headers.Add(col.Header.ToString());
            }

            XBox.ItemsSource = headers;
            YBox.ItemsSource = headers;
        }

        /// <summary>
        /// Plots graphic.
        /// </summary>
        private void PlotButton_OnClick(object sender, RoutedEventArgs e)
        {
            XData = new List<double>(50);
            YData = new List<double>(50);
            if (XBox.SelectedItems.Count == 0 || YBox.SelectedItems.Count == 0)
            {
                return;
            }

            int xIndex = headers.FindIndex(x => x == XBox.SelectedItems[0]?.ToString());
            int yIndex = headers.FindIndex(x => x == YBox.SelectedItems[0]?.ToString());

            foreach (DataRow dataRow in dt.Rows)
            {
                if (!double.TryParse(dataRow.ItemArray[xIndex]?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double cellX))
                {
                    MessageBox.Show("It is not numeric Data");
                    return;
                }
                if (!double.TryParse(dataRow.ItemArray[yIndex]?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double cellY))
                {
                    MessageBox.Show("It is not numeric Data");
                    return;
                }

                YData.Add(cellY);
                XData.Add(cellX);
            }
            XLabel = XBox.SelectedItems[0]?.ToString(); 
            YLabel = YBox.SelectedItems[0]?.ToString();
            new GraphChart(this).Show();
            this.Close();
        }
    }
}
