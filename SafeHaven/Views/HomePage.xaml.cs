using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SafeHaven.Views
{
    public partial class HomePage : ContentPage
    {
        async void GoToMyDocs(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new DocumentListPage());
		}

        public HomePage()
        {
            InitializeComponent();
            Title = "SafeHaven";
            if (!Application.Current.Properties.ContainsKey("ActiveUser"))
            {
                Application.Current.Properties["ActiveUser"] = 1;
            }
            CheckForActiveUser();
        }

        public void CheckForActiveUser()
        {
			if (Application.Current.Properties["ActiveUser"] == null)
			{
				Navigation.PushModalAsync(new LoginPage());
			}
        }

    }
}
