using System;
using System.Collections.Generic;
using SafeHaven.Models;
using Xamarin.Forms;

namespace SafeHaven.Views
{
    public partial class GainAccessPage : ContentPage
    {
		public List<AccessRight> AccessRights { get; set; }

        public GainAccessPage()
        {
            InitializeComponent();
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            var accessright = e.SelectedItem as AccessRight;
            Navigation.PushAsync(new GainAccessDocumentListPage(accessright.Grantor));
            AccessList.SelectedItem = null;
        }

        public async void SetAccessList()
        {
            var response = await App.APIService.GetAccessRightsWhereAccessor((int)Application.Current.Properties["ActiveUser"]);
            if (response.Success)
            {
                AccessRights = response.AccessRights;
                AccessList.ItemsSource = AccessRights;
            }
            else
            {
                await DisplayAlert("Error", response.Message, "Okay");
            }
        }

        protected override void OnAppearing()
        {
            SetAccessList();
        }
    }
}
