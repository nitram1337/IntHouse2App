using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace IntHouse2App.Repository
{
    public class GenericRepository : IGenericRepository
    {
        private HttpClient httpClient;

        public GenericRepository()
        {
            httpClient = new HttpClient();
        }




        #region HELPER
        private void ConfigureHttpClient(string authToken)
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(authToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            }
            else
            {
                httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }
        #endregion
    }
}
