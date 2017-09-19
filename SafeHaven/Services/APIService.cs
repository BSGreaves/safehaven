using System;
using System.Net.Http;
using System.Text;
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
                return new DocumentResponse { Message = "Our database is down at the moment, please try again later", Success = false };
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

		public async Task<JsonResponse> SaveNewDocument(Document newDocument)
		{
			var uri = new Uri(string.Format(_keys.SafeHavenAPI + "/document/post", string.Empty));
			try
			{
				var jsonRequest = JsonConvert.SerializeObject(newDocument);
				var fullRequest = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await _client.PostAsync(uri, fullRequest);
				var jsonResponse = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<JsonResponse>(jsonResponse);
			}
			catch
			{
				throw new NotImplementedException();
			}
		}

        public async Task<SingleDocumentResponse> GetSingleDocument(int docid)
        {
			var uri = new Uri(string.Format(_keys.SafeHavenAPI + "/document/getsingle/" + docid, string.Empty));
			try
			{
				HttpResponseMessage response = await _client.GetAsync(uri);
				var JSONstring = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<SingleDocumentResponse>(JSONstring);
			}
			catch
			{
				throw new NotImplementedException();
			}

        }
    }
}
