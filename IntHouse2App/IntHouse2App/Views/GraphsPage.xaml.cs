using IntHouse2App.ViewModels;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.ChartEntry;

namespace IntHouse2App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GraphsPage : ContentPage
    {
        //List<Entry> entries = new List<Entry>
        //{
        //    new Entry(200)
        //    {
        //        Color = SKColor.Parse("#FF1493"),
        //        Label = "January",
        //        ValueLabel = "200"
        //    },
        //    new Entry(400)
        //    {
        //        Color = SKColor.Parse("#00BFFF"),
        //        Label = "February",
        //        ValueLabel = "400"
        //    },
        //    new Entry(-100)
        //    {
        //        Color = SKColor.Parse("#00CED1"),
        //        Label = "March",
        //        ValueLabel = "200"
        //    }
        //};
        GraphViewModel _viewModel;

        public GraphsPage()
        {
            InitializeComponent();
            //Chart1.Chart = new RadialGaugeChart { Entries = new List<Entry>
            //    {
            //        new Entry(200)
            //        {
            //            Color = SKColor.Parse("#FF1493"),
            //            Label = "January",
            //            ValueLabel = "200"
            //        },
            //        new Entry(400)
            //        {
            //            Color = SKColor.Parse("#00BFFF"),
            //            Label = "February",
            //            ValueLabel = "400"
            //        },
            //        new Entry(-100)
            //        {
            //            Color = SKColor.Parse("#00CED1"),
            //            Label = "March",
            //            ValueLabel = "200"
            //        }
            //    }
            //};
            BindingContext = _viewModel = new GraphViewModel();
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    _viewModel.OnAppearing();
        //}
    }
}