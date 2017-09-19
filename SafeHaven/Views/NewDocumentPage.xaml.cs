using System;
using System.Collections.Generic;
using System.Linq;
using SafeHaven.Models;

using Xamarin.Forms;

namespace SafeHaven.Views
{
    public partial class NewDocumentPage : ContentPage
    {
        async void Cancel(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

		async void Save(object sender, System.EventArgs e)
		{
            var newDoc = (Document)BindingContext;
			if (string.IsNullOrEmpty(newDoc.Title))
			{
				await DisplayAlert("Oops!", "Please give your document a title", "Okay");
                return;
			}
            if (documentTypes.SelectedIndex == -1)
            {
                await DisplayAlert("Oops!", "Please select a document type", "Okay");
                return;
            }
            var doctypetitle = documentTypes.Items[documentTypes.SelectedIndex];
            var doctype = _documentTypes.Single(dt => dt.Title == doctypetitle);
            newDoc.DocumentTypeID = doctype.DocumentTypeID;
            newDoc.UserID = (int)Application.Current.Properties["ActiveUser"];
            newDoc.DateCreated = DateTime.Today;
            JsonResponse response = await App.APIService.SaveNewDocument(newDoc);
            if (response.Success)
            {
                await Navigation.PopModalAsync();
            }
            else
            {
				await DisplayAlert("Error", response.Message, "Okay");
			}
		}

        private IList<DocumentType> _documentTypes;

        void Handle_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // how to get selected index
            var title = documentTypes.Items[documentTypes.SelectedIndex];
            var doctype = _documentTypes.Single(dt => dt.Title == title);
        }

		public async void SetDocumentTypesList()
		{
			var response = await App.APIService.GetDocumentTypes();
			if (response.Success)
			{
				_documentTypes = response.DocumentTypes;
				foreach (var type in _documentTypes)
				{
					documentTypes.Items.Add(type.Title);
				}
			}
			else
			{
				await DisplayAlert("Error", response.Message, "Okay");
			}
		}

        public NewDocumentPage()
        {
            InitializeComponent();
            Title = "New Document";
            SetDocumentTypesList();
            BindingContext = new Document();
        }
    }
}
