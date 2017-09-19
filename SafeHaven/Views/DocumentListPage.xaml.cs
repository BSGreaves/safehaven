using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SafeHaven.Models;
using Xamarin.Forms;

namespace SafeHaven.Views
{
	public partial class DocumentListPage : ContentPage
    {
        async void Handle_Activated(object sender, System.EventArgs e)
        {
			var newPage = new NewDocumentPage();
			await Navigation.PushModalAsync(newPage);
        }

        public List<Document> Documents { get; set; }

		async void NewDocument(object sender, System.EventArgs e)
		{
			if (DocumentList.SelectedItem != null)
			{
                var newPage = new SelectDocumentTypePage();
                await Navigation.PushModalAsync(newPage);
			}
		}

		public DocumentListPage()
        {
            InitializeComponent();
            Title = "My Documents";
            SetDocumentsList();
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
    }
}
