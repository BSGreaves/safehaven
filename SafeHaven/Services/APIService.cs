using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SafeHaven.Models;
using Xamarin.Forms;

namespace SafeHaven.Services
{
    public class APIService
    {
        private HttpClient _client;
		private Keys _keys;


		public APIService()
        {
            _client = new HttpClient();
            _keys = new Keys();
        }

		// GET a list of documents for a userID
		public async Task<DocumentResponse> GetDocuments(int userID)
		{
			var uri = new Uri(string.Format(_keys.SafeHavenAPI + "/document/getall/" + userID, string.Empty));
			try
			{
				HttpResponseMessage response = await _client.GetAsync(uri);
				var JSONstring = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<DocumentResponse>(JSONstring);
			}
			catch
			{
				throw new NotImplementedException();
			}
		}

		public async Task<DocumentTypeResponse> GetDocumentTypes()
		{
			var uri = new Uri(string.Format(_keys.SafeHavenAPI + "/documenttype", string.Empty));
			try
			{
				HttpResponseMessage response = await _client.GetAsync(uri);
				var JSONstring = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<DocumentTypeResponse>(JSONstring);
			}
			catch
			{
				throw new NotImplementedException();
			}
		}
    }
}
