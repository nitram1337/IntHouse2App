using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntHouse2App.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OfflineIndicator : ContentView
    {
        public OfflineIndicator()
        {
            InitializeComponent();
        }
    }
}