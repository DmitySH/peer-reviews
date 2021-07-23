using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows;

namespace DataAnalyzer
{
    /// <summary>
    /// Interaction logic for HistConfig.xaml
    /// </summary>
    public partial class HistConfig : Window
    {
        public List<double> Data { get; set; }
        private readonly MainWindow prev;
        readonly List<string> headers = new();
        private readonly DataTable dt;
        public string YLabel { get; set; }

        /// <summary>
        /// Constructor for config window.
        /// </summary>
        /// <param name="prev"> Previous window. </param>
        public HistConfig(MainWindow prev)
        {
            dt = prev.Dt;
            this.prev = prev;
            InitializeComponent();
            InitializeBox();
        }

        /// <summary>
        /// Initializes box.
        /// </summary>
        private void InitializeBox()
        {
            foreach (var col in prev.CSVGrid.Columns)
            {
                headers.Add(col.Header.ToString());
            }

            Box.ItemsSource = headers;
        }

        /// <summary>
        /// Opens chart.
        /// </summary>
        private void PlotButton_OnClick(object sender, RoutedEventArgs e)
        {
            Data = new List<double>(50);
            if (Box.SelectedItems.Count == 0)
            {
                return;
            }

            int index = headers.FindIndex(x => x == Box.SelectedItems[0]?.ToString());

            foreach (DataRow dataRow in dt.Rows)
            {
                if (!double.TryParse(dataRow.ItemArray[index]?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double cell))
                {
                    MessageBox.Show("It is not numeric Data");
                    return;
                }

                Data.Add(cell);
            }

            YLabel = Box.SelectedItems[0]?.ToString();
            new HistChart(this).Show();
            this.Close();
        }
    }
}
