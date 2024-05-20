using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.Services.Api
{
    public class CommentApiService : ICommentApiService
    {
        private readonly IHttpClientFactory HttpClientFactory;
        private readonly HttpClient HttpClient;

        public CommentApiService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;

            HttpClient = HttpClientFactory.CreateClient();
            HttpClient.BaseAddress = new Uri("https://agileproapi.azurewebsites.net/api/");
            //HttpClient.BaseAddress = new Uri("https://localhost:7226/api/");
        }

        public async Task<CommentDTO> GetByIdAsync(Guid id)
        {
            HttpResponseMessage response = await HttpClient.GetAsync($"Comments/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            string jsonString = await response.Content.ReadAsStringAsync();
            CommentDTO comment = JsonConvert.DeserializeObject<CommentDTO>(jsonString);
            return comment;
        }

        public async Task<List<CommentDTO>> GetAllAsync()
        {
            HttpResponseMessage response = await HttpClient.GetAsync("Comments");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            string jsonString = await response.Content.ReadAsStringAsync();
            List<CommentDTO> comments = JsonConvert.DeserializeObject<List<CommentDTO>>(jsonString);
            return comments;
        }

        public async Task<CommentDTO> CreateAsync(CommentDTO entity)
        {
            string jsonString = JsonConvert.SerializeObject(entity);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await HttpClient.PostAsync("Comments", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            jsonString = await response.Content.ReadAsStringAsync();
            CommentDTO comment = JsonConvert.DeserializeObject<CommentDTO>(jsonString);
            return comment;
        }

        public async Task<CommentDTO> UpdateAsync(Guid id, CommentDTO entity)
        {
            string jsonString = JsonConvert.SerializeObject(entity);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await HttpClient.PutAsync($"Comments/{id}", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            jsonString = await response.Content.ReadAsStringAsync();
            CommentDTO comment = JsonConvert.DeserializeObject<CommentDTO>(jsonString);
            return comment;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            HttpResponseMessage response = await HttpClient.DeleteAsync($"Comments/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            return true;
        }
    }
}
