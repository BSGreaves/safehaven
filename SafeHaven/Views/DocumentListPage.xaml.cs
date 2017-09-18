using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SafeHaven.Models;
using Xamarin.Forms;

namespace SafeHaven.Views
{
	public partial class DocumentListPage : ContentPage
    {
        void NewDocument(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }

        public List<Document> Documents { get; set; }

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
