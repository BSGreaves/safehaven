using System;
using System.Collections.Generic;
using System.IO;
using PCLStorage;
using SafeHaven.Models;
using Xamarin.Forms;

namespace SafeHaven.Views
{
    public partial class NonOwnerDocumentDetailPage : TabbedPage
    {
        private Document _document { get; set; }

        public NonOwnerDocumentDetailPage(Document document)
        {
            InitializeComponent();
            _document = document;
        }

		public async void SetDocument(int docid)
		{
			SingleDocumentResponse response = await App.APIService.GetSingleDocument(docid);
			if (response.Document == null)
			{
				await DisplayAlert("Error", response.Message, "Okay");
			}
			BindingContext = response.Document;
			StackLayout ParentStack = new StackLayout();
			if (response.Document.DocumentImages != null)
			{
				foreach (DocumentImage img in response.Document.DocumentImages)
				{
					StackLayout stack = new StackLayout();
					Image image = new Image()
					{
						Aspect = Aspect.AspectFit,
					};
					IFile file = await FileSystem.Current.GetFileFromPathAsync(img.FilePath);
					if (file != null)
					{
						Stream stream = await file.OpenAsync(PCLStorage.FileAccess.Read);
						image.Source = ImageSource.FromStream(() => stream);
						stack.Children.Add(image);
						ParentStack.Children.Add(stack);
					}
				}
				ImageScroller.Content = ParentStack;
			}
			if (response.Document.DocumentImages == null)
			{
				StackLayout stack = new StackLayout();
				Label label = new Label { Text = "This user hasn't taken any pictures yet", HorizontalTextAlignment = TextAlignment.Center };
			}
		}

		protected override void OnAppearing()
		{
			SetDocument(_document.DocumentID);
		}
    }
}
