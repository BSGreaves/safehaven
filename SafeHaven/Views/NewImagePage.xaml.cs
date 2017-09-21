using System;
using System.Collections.Generic;
using SafeHaven.Models;
using Plugin.Media;
using Xamarin.Forms;
using System.IO;
using SafeHaven.ViewModels;

namespace SafeHaven.Views
{
    public partial class NewImagePage : ContentPage
    {
        private Document _document { get; set; }

        private Plugin.Media.Abstractions.MediaFile _file { get; set; }

        public NewImagePage(Document document)
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

				image.Source = ImageSource.FromStream(() =>
				{
					var stream = file.GetStream();
					return stream;
				});
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
            var filebytes = File.ReadAllBytes(_file.Path);
            var file = new ByteFile
            {
                File = filebytes,
                DocumentID = _document.DocumentID
            };

            //var response = App.APIService.SaveNewPhoto(_file, _document.DocumentID);
			//await DisplayAlert("Oops!", "", "OK");

		}
    }
}
