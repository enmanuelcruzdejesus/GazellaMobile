using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GazellaMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WhenThereIsNoConnectionPage : ContentPage
	{
		public WhenThereIsNoConnectionPage ()
		{
			InitializeComponent ();
		}
	}
}