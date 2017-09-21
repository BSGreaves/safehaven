using System;
using System.Collections.Generic;
using SafeHaven.Models;
using Plugin.Media;
using Xamarin.Forms;
using System.IO;
using SafeHaven.ViewModels;

namespace SafeHaven.Views
{
    public partial class NewPhotoTest : ContentPage
    {
		private Document _document { get; set; }

		private Plugin.Media.Abstractions.MediaFile _file { get; set; }

		public NewPhotoTest(Document document)
		{
			InitializeComponent();
			_document = document;

			takePhoto.Clicked += async (sender, args) =>
			{

				if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
				{
					await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
					return;
				}

				var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
				{
					PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
					Directory = "Sample",
					Name = "test.jpg",
					SaveToAlbum = true
				});

				if (file == null)
					return;

				await DisplayAlert("File Location", file.Path, "OK");

				_file = file;

                imagepath.Text = _file.Path;

                //file.Dispose();
			};
		}

		async void Cancel(object sender, System.EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		async void Save(object sender, System.EventArgs e)
		{
			if (_file == null)
			{
				await DisplayAlert("Oops!", "Take a photo first", "OK");
				return;
			}
            DocumentImage newdocumentimage = new DocumentImage
            {
                DocumentID = _document.DocumentID,
                FilePath = _file.Path,
                DateCreated = DateTime.Today,
                PageNumber = 1
            };
			var response = App.APIService.SaveNewPhoto(newdocumentimage);
            //if (response.Success != null)
            //{
            //    await DisplayAlert("Oops!", response.Message, "OK");
            //    return;
            //}
            //await Navigation.PopModalAsync();
		}
    }
}
