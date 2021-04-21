using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Entry = Microcharts.ChartEntry;

namespace IntHouse2App.ViewModels
{
    public class GraphViewModel : BaseViewModel
    {
        public Chart TestChart { get; set; }

        public GraphViewModel()
        {
            TestChart = new LineChart
            {
                Entries = new List<Entry>
            {
                new Entry(200)
                {
                    Color = SKColor.Parse("#FF1493"),
                    Label = "January",
                    ValueLabel = "200"
                },
                new Entry(400)
                {
                    Color = SKColor.Parse("#00BFFF"),
                    Label = "February",
                    ValueLabel = "400"
                },
                new Entry(-100)
                {
                    Color = SKColor.Parse("#00CED1"),
                    Label = "March",
                    ValueLabel = "200"
                }
            }
            };
        }
    }
}
