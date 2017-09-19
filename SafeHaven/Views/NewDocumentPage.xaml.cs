using System;
using System.Collections.Generic;
using System.Linq;
using SafeHaven.Models;

using Xamarin.Forms;

namespace SafeHaven.Views
{
    public partial class NewDocumentPage : ContentPage
    {

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
        }
    }
}
