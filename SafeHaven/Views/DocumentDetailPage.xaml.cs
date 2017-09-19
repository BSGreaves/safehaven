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
            _document = document;
			//BindingContext = _document ?? throw new ArgumentNullException();

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
		}

		public async void SetDocument(int docid)
		{
			var response = await App.APIService.GetSingleDocument(docid);
            if (response.Success)
            {
                BindingContext = response.Document;
				StackLayout ParentStack = new StackLayout();
				foreach (DocumentImage img in response.Document.DocumentImages)
				{
					var stack = new StackLayout();
					Image image = new Image()
					{
						Aspect = Aspect.AspectFit,
						Source = new UriImageSource { Uri = new Uri(img.FilePath) },

					};
					Label label = new Label { Text = "Page {img.PageNumber}", HorizontalTextAlignment = TextAlignment.Center };
					stack.Children.Add(label);
					stack.Children.Add(image);
					ParentStack.Children.Add(stack);
				}
				ImageScroller.Content = ParentStack;
            }
            else
            {
                await DisplayAlert("Error", response.Message, "Okay");
            }
		}

		protected override void OnAppearing()
		{
            
			SetDocument(_document.DocumentID);
		}
	}
}
