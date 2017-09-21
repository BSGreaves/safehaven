using System;
using System.Collections.Generic;
using System.IO;
using PCLStorage;
using SafeHaven.Models;
using SafeHaven.Services;
using Xamarin.Forms;

namespace SafeHaven.Views
{
    public partial class DocumentDetailPage : TabbedPage
    {

		private Document _document { get; set; }

        private Keys _keys;

        public DocumentDetailPage(Document document)
        {
			_keys = new Keys();
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
					//var uri = new Uri(string.Format(_keys.SafeHavenAPI + "/documentimage/get/" + img.FilePath, string.Empty));
					Image image = new Image()
					{
						Aspect = Aspect.AspectFit,
					};
					IFile file = await FileSystem.Current.GetFileFromPathAsync(img.FilePath);
                    if (file != null)
                    {
                        Stream stream = await file.OpenAsync(PCLStorage.FileAccess.Read);
                        image.Source = ImageSource.FromStream(() => stream);
                        //Label label = new Label { Text = "Page " + img.PageNumber, HorizontalTextAlignment = TextAlignment.Center };
                        //stack.Children.Add(label);
                        stack.Children.Add(image);
                        ParentStack.Children.Add(stack);
                    }
                }
				ImageScroller.Content = ParentStack;
            }
            if (response.Document.DocumentImages == null)
            {
                StackLayout stack = new StackLayout();
				Label label = new Label { Text = "You haven't taken any pictures yet", HorizontalTextAlignment = TextAlignment.Center };

			}
		}

		protected override void OnAppearing()
		{
			SetDocument(_document.DocumentID);
		}

		async void NewImage(object sender, System.EventArgs e)
		{
			await Navigation.PushModalAsync(new NewPhotoTest(_document));
		}
	}
}
