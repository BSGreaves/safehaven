using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SafeHaven.Models;
using Xamarin.Forms;

namespace SafeHaven.Views
{
	public partial class DocumentListPage : ContentPage
    {
        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
			if (e.SelectedItem == null)
			{
				return;
			}
			var document = e.SelectedItem as Document;
            Navigation.PushAsync(new DocumentDetailPage(document));
			DocumentList.SelectedItem = null;
        }

        async void NewDocument(object sender, System.EventArgs e)
        {
            
            var newPage = new NewDocumentPage();
			await Navigation.PushModalAsync(newPage);
        }

        public List<Document> Documents { get; set; }

		public DocumentListPage()
        {
            InitializeComponent();
        }

        public async void SetDocumentsList()
        {
            var response = await App.APIService.GetDocuments((int)Application.Current.Properties["ActiveUser"]);
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

		async void Delete_Clicked(object sender, System.EventArgs e)
		{
			var document = (sender as MenuItem).BindingContext as Document;
            var response = await App.APIService.DeleteDocument(document.DocumentID);
            if (response.Success)
            {
				SetDocumentsList();
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
