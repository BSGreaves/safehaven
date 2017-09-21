using System;
using System.Collections.Generic;
using SafeHaven.Models;
using SafeHaven.ViewModels;
using Xamarin.Forms;

namespace SafeHaven.Views
{
    public partial class NewAccessRightPage : ContentPage
    {
        public NewAccessRightPage()
        {
            InitializeComponent();
            BindingContext = new NewAccessRight();
        }

		async void Cancel(object sender, System.EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		async void Save(object sender, System.EventArgs e)
		{
			var newaccessright = (NewAccessRight)BindingContext;
			if (string.IsNullOrEmpty(newaccessright.AccessorEmail))
			{
				await DisplayAlert("Oops!", "Please type in an email", "Okay");
				return;
			}
            newaccessright.GrantorUserID = (int)Application.Current.Properties["ActiveUser"];
			JsonResponse response = await App.APIService.SaveNewAccessRight(newaccessright);
			if (response.Success)
			{
				await Navigation.PopModalAsync();
			}
			else
			{
				await DisplayAlert("Error", response.Message, "Okay");
			}
		}

	}
}
