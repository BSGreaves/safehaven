using System;
using SafeHaven.Views;
using Xamarin.Forms;

namespace SafeHaven
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new MasterPage();
        }
    }
}
