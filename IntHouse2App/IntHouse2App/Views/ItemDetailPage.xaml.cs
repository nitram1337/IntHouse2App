using IntHouse2App.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace IntHouse2App.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}