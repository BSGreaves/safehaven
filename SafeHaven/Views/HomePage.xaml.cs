using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SafeHaven.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            Title = "SafeHaven";
            if (!Application.Current.Properties.ContainsKey("ActiveUser"))
            {
                Application.Current.Properties["ActiveUser"] = 1;
            }
        }
    }
}
