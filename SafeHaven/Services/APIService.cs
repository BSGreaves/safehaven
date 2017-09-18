using System;
using System.Net.Http;

namespace SafeHaven.Services
{
    public class APIService
    {
        private HttpClient _client;

        public APIService()
        {
            _client = new HttpClient();
        }
    }
}
