using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SafeHaven.Views
{
    public partial class LogoutPage : ContentPage
    {
        async void Cancel(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

		void Logout(object sender, System.EventArgs e)
		{
			throw new NotImplementedException();
		}

        public LogoutPage()
        {
            InitializeComponent();
        }
    }
}
