using System;
using System.Collections.Generic;
using SafeHaven.ViewModels;
using Xamarin.Forms;

namespace SafeHaven.Views
{
    public partial class MasterPage : MasterDetailPage
    {
        private MasterPage _ThisPage { get; set; }

		public MasterPage()
		{
			InitializeComponent();
            _ThisPage = this;
			navBarPage.NavBarList.ItemSelected += OnItemSelected;
			navBarPage.BackgroundColor = Color.FromHex("#400000");
			navBarPage.NavBarList.BackgroundColor = Color.FromHex("#400000");
		}

		void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as NavBarItem;
			if (item != null)
			{
				Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
				navBarPage.NavBarList.SelectedItem = null;
				IsPresented = false;
			}
		}
    }
}
