namespace interbanque.Services
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using interbanque.DTO;
    using Newtonsoft.Json;

    public class ApiServiceExtern
    {
        private readonly HttpClient _httpClient;
         
        public ApiServiceExtern(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

         
        public async Task<responseModel> PostDataAsync<T>(string url, T data)
        {
            
            var jsonData = JsonConvert.SerializeObject(data);

       
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

          
            var response = await _httpClient.PostAsync(url, content);

           
            if (response.IsSuccessStatusCode)
            {
               
              var result= await response.Content.ReadAsStringAsync();
              return   JsonConvert.DeserializeObject<responseModel>(result);
            }
 
            return new responseModel() {keyResponse="erreur_sending_data",message= $"Error: {response.StatusCode}" } ;
        }
    }

}
