using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using CsvHelper;

namespace DataAnalyzer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DataTable Dt { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Closes main window.
        /// </summary>
        private void CloseWindow_OnClick(object sender, RoutedEventArgs e) => this.Close();

        /// <summary>
        /// Opens CSV table.
        /// </summary>
        private void OpenCSV_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "CSV files(*.csv)|*.csv";
                bool? result = openFileDialog.ShowDialog();

                if (result != null && result == true)
                {
                    ReadData(openFileDialog);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can't load Data");
            }
        }


        /// <summary>
        /// Reads Data in CSV.
        /// </summary>
        /// <param name="openFileDialog"> Open file dialog. </param>
        private void ReadData(OpenFileDialog openFileDialog)
        {
            using (TextReader tr = new StreamReader(openFileDialog.FileName))
            {
                CsvParser csv = new CsvParser(tr, CultureInfo.InvariantCulture);
                csv.Read();
                Dt = new DataTable();
                var save = csv.Record;
                // Creates header and columns.
                for (int i = 0; i < save.Length; i++)
                {
                    save[i] = string.IsNullOrEmpty(save[i]) ? "_" : save[i];
                    save[i] = save[i].Replace("/", @"\");

                    Dt.Columns.Add(save[i]);
                }

                // Creates rows.
                while (csv.Read())
                {
                    Dt.Rows.Add(csv.Record);
                }

                CSVGrid.ItemsSource = Dt.DefaultView;
            }
        }

        /// <summary>
        /// Plots histogram.
        /// </summary>
        private void CreateHist_OnClick(object sender, RoutedEventArgs e)
        {
            if (CSVGrid.ItemsSource == null)
            {
                MessageBox.Show("Load CSV firstly.");
                return;
            }
            new HistConfig(this).Show();
        }

        /// <summary>
        /// Plots graphic.
        /// </summary>
        private void Create2D_OnClick(object sender, RoutedEventArgs e)
        {
            if (CSVGrid.ItemsSource == null)
            {
                MessageBox.Show("Load CSV firstly.");
                return;
            }
            new GraphConfig(this).Show();
        }

        /// <summary>
        /// Makes analysis.
        /// </summary>
        private void Analyze_OnClick(object sender, RoutedEventArgs e)
        {
            if (CSVGrid.ItemsSource == null)
            {
                MessageBox.Show("Load CSV firstly.");
                return;
            }

            var data = new Dictionary<string, List<double>>();
            GetNumeric(data);

            foreach (var col in data)
            {
                col.Value.Sort();
                double avg = GetAVG(col);
                MessageBox.Show($"{col.Key}:{Environment.NewLine}" +
                                $"Median: {GetMedian(col):F3}{Environment.NewLine}" +
                                $"Average: {avg:F3}{Environment.NewLine}" +
                                $"Standart deviation: {GetAV(avg, col):F3}{Environment.NewLine}" +
                                $"Dispersion: {GetDispersion(avg, col):F3}{Environment.NewLine}");
            }
        }

        /// <summary>
        /// Finds dispertion.
        /// </summary>
        /// <param name="avg"> Average of column. </param>
        /// <param name="col"> Column. </param>
        /// <returns> Dispertion. </returns>
        private static double GetDispersion(double avg, KeyValuePair<string, List<double>> col)
        {
            double dis = 0;
            foreach (var val in col.Value)
            {
                dis += Math.Pow(val - avg, 2);
            }
            return dis /col.Value.Count;
        }

        /// <summary>
        /// Finds standart deviation.
        /// </summary>
        /// <param name="avg"> Average of column. </param>
        /// <param name="col"> Column. </param>
        /// <returns> Standart deviation. </returns>
        private static double GetAV(double avg, KeyValuePair<string, List<double>> col)
        {
            double av = 0;
            foreach (var val in col.Value)
            {
                av += Math.Pow(val - avg, 2);
            }

            return Math.Sqrt(av / col.Value.Count);
        }

        /// <summary>
        /// Finds median. 
        /// </summary>
        /// <param name="col"> Column. </param>
        /// <returns> Median. </returns>
        private static double GetMedian(KeyValuePair<string, List<double>> col)
        {
            double median;
            if (col.Value.Count % 2 == 0)
            {
                median = (col.Value[col.Value.Count / 2 - 1] + col.Value[col.Value.Count / 2]) / 2;
            }
            else
            {
                median = col.Value[col.Value.Count / 2];
            }

            return median;
        }

        /// <summary>
        /// Finds average.
        /// </summary>
        /// <param name="col"> Column. </param>
        /// <returns> Average value. </returns>
        private static double GetAVG(KeyValuePair<string, List<double>> col)
        {
            double avg = 0;
            foreach (var val in col.Value)
            {
                avg += val;
            }

            return avg / col.Value.Count;
        }

        /// <summary>
        /// Finds all columns with numeric values. 
        /// </summary>
        /// <param name="data"> Dictionary with header's name and data of it. </param>
        private void GetNumeric(Dictionary<string, List<double>> data)
        {
            for (int i = 0; i < Dt.Columns.Count; i++)
            {
                string header = CSVGrid.Columns[i].Header.ToString();
                data.Add(header, new List<double>());
                foreach (DataRow dataRow in Dt.Rows)
                {
                    if (!double.TryParse(dataRow.ItemArray[i]?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture,
                        out double cell))
                    {
                        data.Remove(header);
                        break;
                    }

                    data[header].Add(cell);
                }
            }
        }
    }
}
