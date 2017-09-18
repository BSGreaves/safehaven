using System;
using SafeHaven.Services;
using SafeHaven.Views;
using Xamarin.Forms;

namespace SafeHaven
{
    public partial class App : Application
    {
        public static APIService APIService { get; private set; }
        public App()
        {
            InitializeComponent();
            APIService = new APIService();
            MainPage = new MasterPage();

        }
    }
}
