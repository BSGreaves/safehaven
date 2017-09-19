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

            List<DocumentImage> images = new List<DocumentImage>
            {
                new DocumentImage
                {
                    DocumentImageID = 1,
                    DocumentID = 1,
                    PageNumber = 1,
                    FilePath = "http://www.falsof.com/images/Document_Mutual_Release.gif"
                },
				new DocumentImage
				{
					DocumentImageID = 2,
					DocumentID = 1,
					PageNumber = 3,
					FilePath = "http://www.falsof.com/images/Document_Mutual_Release.gif"
				},
				new DocumentImage
				{
					DocumentImageID = 3,
					DocumentID = 1,
					PageNumber = 3,
					FilePath = "http://www.falsof.com/images/Document_Mutual_Release.gif"
				}
            };

            StackLayout ParentStack = new StackLayout();
            foreach (DocumentImage img in images)
            {
				var stack = new StackLayout();
                Image image = new Image()
                {
                    Aspect = Aspect.AspectFit,
                    Source = new UriImageSource { Uri = new Uri(img.FilePath) }
                };
                Label label = new Label { Text = "Page {img.PageNumber}", HorizontalTextAlignment = TextAlignment.Center};
                stack.Children.Add(label);
                stack.Children.Add(image);
                ParentStack.Children.Add(stack);
            }
            ImageScroller.Content = ParentStack;

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
