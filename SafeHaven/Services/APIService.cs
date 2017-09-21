using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SafeHaven.Models;
using SafeHaven.ViewModels;
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
                SingleDocumentResponse result = JsonConvert.DeserializeObject<SingleDocumentResponse>(JSONstring);
                return result;
			}
			catch
			{
				throw new NotImplementedException();
			}
        }

		public async Task<SingleUserResponse> Login(User user)
		{
			var uri = new Uri(string.Format(_keys.SafeHavenAPI + "/user/login", string.Empty));
			try
			{
				var jsonRequest = JsonConvert.SerializeObject(user);
				var fullRequest = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await _client.PostAsync(uri, fullRequest);
				var jsonResponse = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<SingleUserResponse>(jsonResponse);
			}
			catch
			{
				throw new NotImplementedException();
			}
		}

		public async Task<SingleUserResponse> Register(User user)
		{
			var uri = new Uri(string.Format(_keys.SafeHavenAPI + "/user/register", string.Empty));
			try
			{
				var jsonRequest = JsonConvert.SerializeObject(user);
				var fullRequest = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await _client.PostAsync(uri, fullRequest);
				var jsonResponse = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<SingleUserResponse>(jsonResponse);
			}
			catch
			{
				throw new NotImplementedException();
			}
		}

   //     public async Task<JsonResponse> SaveNewPhoto(Plugin.Media.Abstractions.MediaFile file, int docid)
   //     {
   //         var uri = new Uri(string.Format(_keys.SafeHavenAPI + "/documentimage/post/" + docid, string.Empty));
   //         try
   //         {
   //             var content = new MultipartFormDataContent();
   //             content.Add(new StreamContent(file.GetStream()), "newimage");
   //             HttpResponseMessage response = await _client.PostAsync(uri, content);
   //             var JSONstring = await response.Content.ReadAsStringAsync();
   //             JsonResponse result = JsonConvert.DeserializeObject<JsonResponse>(JSONstring);
   //             return result;
   //         }
			//catch
			//{
			//	throw new NotImplementedException();
			//}
        //}

		//public async Task<JsonResponse> SaveNewPhoto(ByteFile file)
		//{
		//	var uri = new Uri(string.Format(_keys.SafeHavenAPI + "/documentimage/post/", string.Empty));
		//	try
		//	{
		//		HttpResponseMessage response = await _client.PostAsync(uri, file);
		//		var JSONstring = await response.Content.ReadAsStringAsync();
		//		JsonResponse result = JsonConvert.DeserializeObject<JsonResponse>(JSONstring);
		//		return result;
		//	}
		//	catch
		//	{
		//		throw new NotImplementedException();
		//	}
		//}
    }
}
