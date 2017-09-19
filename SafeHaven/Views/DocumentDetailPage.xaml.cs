using System;
using System.Collections.Generic;
using SafeHaven.Models;
using Xamarin.Forms;

namespace SafeHaven.Views
{
    public partial class DocumentDetailPage : TabbedPage
    {

		private Document _document { get; set; }

        public DocumentDetailPage(Document document)
        {
            InitializeComponent();
            BindingContext = document ?? throw new ArgumentNullException();
        }

		//public async void SetDocument()
		//{
		//	var response = await App.APIService.GetSingleDocument();
		//	if (response.Success)
		//	{
		//		Documents = response.Documents;
		//		DocumentList.ItemsSource = Documents;
		//	}
		//	else
		//	{
		//		await DisplayAlert("Error", response.Message, "Okay");
		//	}
		//}
	}
}
