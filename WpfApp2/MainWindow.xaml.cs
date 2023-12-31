﻿using Microsoft.Win32;
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
using _2023_WpfApp2;
using System.Net.Http;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string url = "https://data.moenv.gov.tw/api/v2/aqx_p_432?api_key=e8dd42e6-9b8b-43f8-991e-b3dee723a52d&limit=1000&sort=ImportDate%20desc&format=JSON";
        AQIdata aqidata = new AQIdata();
        List<Field> fields = new List<Field>();//欄位的定義
        List<Record> records = new List<Record>();//多少筆記錄
        List<Record> selectedRecords = new List<Record>();
        SeriesCollection seriesCollection = new SeriesCollection();

        public MainWindow()
        {
            InitializeComponent();
            UrlTextBox.Text= url;
            selectedRecords.Clear();
        }

        private async void GetWebDataBtn_Click(object sender, RoutedEventArgs e)// await async 非同步
        {
            ContentTextBox.Text= "正在抓取資料......";
            string jsontext = await FetchContentAsync(url);
            ContentTextBox.Text = jsontext;
            aqidata =JsonSerializer.Deserialize<AQIdata>(jsontext);
            fields= aqidata.fields.ToList();
            records = aqidata.records.ToList();
            selectedRecords = records;
            StatusTextBlock.Text = $"共有 {records.Count} 筆資料";
            DisplayAQIData();
        }

        private void DisplayAQIData()
        {
            RecordDataGrid.ItemsSource = records;
            DataWrapPanel.Children.Clear();
            foreach(var field in fields)
            {
                var propertyInfo = typeof(Record).GetProperty(field.id);
                if(propertyInfo != null )
                {
                    var value= propertyInfo.GetValue(records[0]) as string;
                    if(double.TryParse(value, out double v))
                    {
                        CheckBox cb = new CheckBox()
                        {
                            Content = field.info.label,
                            Tag = field.id,
                            Margin = new Thickness(5),
                            Width = 120,
                            FontSize = 14,
                            FontWeight = FontWeights.Bold,
                        };
                        DataWrapPanel.Children.Add(cb);
                    }
                }
            }
        }

        private async Task<string> FetchContentAsync(string url)
        {
            try
            {
                using (HttpClient client=new HttpClient())
                {
                    return await client.GetStringAsync(url);
                }
            }
            catch (Exception ex)
            {
                return $"發生錯誤: {ex.Message}";
            }
        }

        private void RecordDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void RecordDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {

        }
    }
}
