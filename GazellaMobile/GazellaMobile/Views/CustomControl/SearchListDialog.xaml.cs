using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GazellaMobile.Views.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchListDialog : ContentView
    {
        
        public string Result { get; set; }
        public EventHandler<ItemTappedEventArgs> TappedEvent;
        public SearchListDialog(dynamic[] data)
        {
            InitializeComponent();
            this.searchDialogListView.ItemsSource = data;
            this.searchDialogListView.ItemTapped += searchDialogListView_ItemTapped;
            
        }

       

        private void searchListSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void searchDialogListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            TappedEvent?.Invoke(this, e);
        }
    }
}