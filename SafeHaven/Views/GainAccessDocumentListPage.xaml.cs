using System;
using System.Collections.Generic;
using SafeHaven.Models;
using Xamarin.Forms;

namespace SafeHaven.Views
{
    public partial class GainAccessDocumentListPage : ContentPage
    {
        private User _user { get; set; }

		public List<Document> Documents { get; set; }

		public GainAccessDocumentListPage(User user)
        {
            InitializeComponent();
            _user = user;
        }

		void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
			{
				return;
			}
			var document = e.SelectedItem as Document;
			Navigation.PushAsync(new NonOwnerDocumentDetailPage(document));
			DocumentList.SelectedItem = null;
		}

		public async void SetDocumentsList()
		{
			var response = await App.APIService.GetDocuments(_user.UserID);
			if (response.Success)
			{
				Documents = response.Documents;
				DocumentList.ItemsSource = Documents;
			}
			else
			{
				await DisplayAlert("Error", response.Message, "Okay");
			}
		}

		protected override void OnAppearing()
		{
			SetDocumentsList();
		}
    }
}
