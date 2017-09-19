using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SafeHaven.Views
{
    public partial class SelectDocumentTypePage : ContentPage
    {
		public ListView DocumentTypes { get { return listView; } }
		public SelectDocumentTypePage()
		{
			InitializeComponent();

			listView.ItemsSource = new List<string>
			{
				"None",
				"Email",
				"SMS"
			};
        }
    }
}
