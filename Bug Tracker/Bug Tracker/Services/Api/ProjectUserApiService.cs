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
    public class ProjectUserApiService : IProjectUserApiService
    {
        private readonly IHttpClientFactory HttpClientFactory;
        private readonly HttpClient HttpClient;

        public ProjectUserApiService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;

            HttpClient = HttpClientFactory.CreateClient();
            HttpClient.BaseAddress = new Uri("https://agileproapi.azurewebsites.net/api/");
            //HttpClient.BaseAddress = new Uri("https://localhost:7226/api/");
        }

        public async Task<ProjectUserDTO> GetByProjectAndUserIdAsync(Guid projectId, string userId)
        {
            HttpResponseMessage response = await HttpClient.GetAsync($"Projects/{projectId}/Users/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            string jsonString = await response.Content.ReadAsStringAsync();
            ProjectUserDTO projectUser = JsonConvert.DeserializeObject<ProjectUserDTO>(jsonString);
            return projectUser;
        }

        public async Task<List<ProjectUserDTO>> GetAllAsync()
        {
            HttpResponseMessage response = await HttpClient.GetAsync("ProjectUsers");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            string jsonString = await response.Content.ReadAsStringAsync();
            List<ProjectUserDTO> projectUsers = JsonConvert.DeserializeObject<List<ProjectUserDTO>>(jsonString);
            return projectUsers;
        }

        public async Task<ProjectUserDTO> CreateAsync(Guid projectId, ProjectUserDTO projectUserDTO)
        {
            string jsonString = JsonConvert.SerializeObject(projectUserDTO);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await HttpClient.PostAsync($"Projects/{projectId}/Users", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            jsonString = await response.Content.ReadAsStringAsync();
            ProjectUserDTO projectUser = JsonConvert.DeserializeObject<ProjectUserDTO>(jsonString);
            return projectUser;
        }

        public async Task<ProjectUserDTO> UpdateAsync(Guid projectId, string userId, ProjectUserDTO projectUserDTO)
        {
            string jsonString = JsonConvert.SerializeObject(projectUserDTO);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await HttpClient.PutAsync($"Projects/{projectId}/Users/{userId}", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            jsonString = await response.Content.ReadAsStringAsync();
            ProjectUserDTO projectUser = JsonConvert.DeserializeObject<ProjectUserDTO>(jsonString);
            return projectUser;
        }

        public async Task<bool> DeleteAsync(int projectId, string userId)
        {
            HttpResponseMessage response = await HttpClient.DeleteAsync($"Projects/{projectId}/Users/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            return true;
        }
    }
}
