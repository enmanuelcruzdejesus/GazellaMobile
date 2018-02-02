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
        dynamic[] _data;
        public string Result { get; set; }
        public EventHandler<ItemTappedEventArgs> TappedEvent;
        public SearchListDialog(dynamic[] data)
        {
            InitializeComponent();
            _data = data;
            this.searchDialogListView.ItemsSource = _data;
            this.searchDialogListView.ItemTapped += searchDialogListView_ItemTapped;
            
        }
      
        private void searchListSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyboard = searchListSearchBar.Text;
            var searchResult = _data.Where(s => s.Detail.ToLower().Contains(keyboard.ToLower()));
            this.searchDialogListView.ItemsSource = searchResult;
        }

        private void searchDialogListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            TappedEvent?.Invoke(this, e);
        }
    }
}