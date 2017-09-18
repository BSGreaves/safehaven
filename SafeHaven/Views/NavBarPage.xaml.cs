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
                    Title = "My Documents",
                    IconSrc = "",
                    TargetType = typeof(DocumentListPage)
                },
                new NavBarItem
                {
                    Title = "New Document",
                    IconSrc = "",
                    TargetType = typeof(NewDocumentPage)
                },
                new NavBarItem
                {
                    Title = "Share and Access",
                    IconSrc = "",
                    TargetType = typeof(ShareAccessPage)
                },
                new NavBarItem
                {
                    Title = "Profile",
                    IconSrc = "",
                    TargetType = typeof(ProfilePage)
                }
            };

            navbarlist.ItemsSource = NavBarItems;
            
        }
    }
}
