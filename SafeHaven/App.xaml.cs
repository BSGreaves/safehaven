using System;
using SafeHaven.Views;
using Xamarin.Forms;

namespace SafeHaven
{
    public partial class App : Application
    {
        public NavigationPage NavigationPage { get; private set; }

        public App()
        {
            InitializeComponent();
            MainPage = new MasterPage();

        }
    }
}
