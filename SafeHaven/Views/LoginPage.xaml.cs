using System;
using System.Collections.Generic;
using SafeHaven.Models;
using Xamarin.Forms;

namespace SafeHaven.Views
{
    public partial class LoginPage : TabbedPage
    {
        async void Login(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(emailLogin.Text))
            {
				await DisplayAlert("Oops!", "Please enter your email address", "OK");
			}
			if (string.IsNullOrEmpty(passLogin.Text))
			{
				await DisplayAlert("Oops!", "Please enter your password", "OK");
			}
            User thisuser = new User
            {
                Email = emailLogin.Text,
                Password = passLogin.Text
            };
            var response = await App.APIService.Login(thisuser);
            if (response.Success)
            {
                Application.Current.Properties["ActiveUser"] = response.User.UserID;
                await Navigation.PopModalAsync();
            }
            else
            {
				await DisplayAlert("Oops!", response.Message, "OK");
			}
        }

        async void Register(object sender, System.EventArgs e)
        {
			{
				if (string.IsNullOrEmpty(emailRegister.Text))
				{
					await DisplayAlert("Oops!", "Please enter your email address", "OK");
				}
				if (string.IsNullOrEmpty(passRegister.Text))
				{
					await DisplayAlert("Oops!", "Please enter your password", "OK");
				}
				User thisuser = new User
				{
					Email = emailRegister.Text,
					Password = passRegister.Text
				};
				var response = await App.APIService.Register(thisuser);
				if (response.Success)
				{
					Application.Current.Properties["ActiveUser"] = response.User.UserID;
					await Navigation.PopModalAsync();
				}
				else
				{
					await DisplayAlert("Oops!", response.Message, "OK");
				}
			}
        }

        public LoginPage()
        {
            InitializeComponent();
        }
    }
}
