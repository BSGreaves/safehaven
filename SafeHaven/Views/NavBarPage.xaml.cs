using System;
using System.Collections.Generic;
using SafeHaven.ViewModels;
using Xamarin.Forms;

namespace SafeHaven.Views
{
    public partial class NavBarPage : ContentPage
    {

         public ListView NavBarList { get { return navbarlist; } }

		public NavBarPage()
        {
			InitializeComponent();
			Title = "Menu";

            var NavBarItems = new List<NavBarItem>
            {
                new NavBarItem
                {
                    Title = "My Docs",
                    IconSrc = "mydocs.png",
                    TargetType = typeof(DocumentListPage),
                    TextColor = "#ffffff"
                },
                new NavBarItem
                {
                    Title = "Grant Access",
                    IconSrc = "grantaccess.png",
                    TargetType = typeof(GrantAccessPage),
					TextColor = "#ffffff"

				},
                new NavBarItem
                {
                    Title = "Gain Access",
                    IconSrc = "gainaccess.png",
                    TargetType = typeof(GainAccessPage),
					TextColor = "#ffffff"

				},
                new NavBarItem
                {
                    Title = "Profile",
                    IconSrc = "profile.png",
                    TargetType = typeof(ProfilePage),
					TextColor = "#ffffff"
				},
				new NavBarItem
				{
					Title = "Logout",
					IconSrc = "logout.png",
					TargetType = typeof(HomePage),
					TextColor = "#ffffff"
				}
            };

            navbarlist.ItemsSource = NavBarItems;
            
        }
    }
}
