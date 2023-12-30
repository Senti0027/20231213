using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Net.Http.Json;
using LiveCharts;
using _2023_WpfApp6;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string url = "https://data.moenv.gov.tw/api/v2/aqx_p_432?api_key=e8dd42e6-9b8b-43f8-991e-b3dee723a52d&limit=1000&sort=ImportDate%20desc&format=JSON";
        AQIdata aqidata = new AQIdata();
        List<Field> fields = new List<Field>();
        List<Record> records = new List<Record>();
        List<Record> selectedRecords = new List<Record>();
        SeriesCollection seriesCollection = new SeriesCollection();

        public MainWindow()
        {
            InitializeComponent();
            UrlTextBox.Text= url;
            selectedRecords.Clear();
        }

        private void GetWebDataBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RecordDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void RecordDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {

        }
    }
}
