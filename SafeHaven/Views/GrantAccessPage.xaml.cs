using System;
using System.Collections.Generic;
using SafeHaven.Models;
using Xamarin.Forms;

namespace SafeHaven.Views
{
    public partial class GrantAccessPage : ContentPage
    {
		public List<AccessRight> AccessRights { get; set; }

		public GrantAccessPage()
		{
			InitializeComponent();
		}
		
        async void NewAccessRight(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new NewAccessRightPage());
        }

		public async void SetAccessList()
		{
			var response = await App.APIService.GetAccessRightsWhereGrantor((int)Application.Current.Properties["ActiveUser"]);
			if (response.Success)
			{
				AccessRights = response.AccessRights;
				AccessList.ItemsSource = AccessRights;
			}
			else
			{
				await DisplayAlert("Error", response.Message, "Okay");
			}
		}

		protected override void OnAppearing()
		{
			SetAccessList();
		}
    }
}
