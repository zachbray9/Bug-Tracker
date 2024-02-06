using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.Services.Api
{
    public class ProjectUserApiService : IApiService<ProjectUserDTO>
    {
        private readonly IHttpClientFactory HttpClientFactory;
        private readonly HttpClient HttpClient;

        public ProjectUserApiService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;

            HttpClient = HttpClientFactory.CreateClient();
            HttpClient.BaseAddress = new Uri("https://bugtrackerapi.azurewebsites.net/api/");
        }

        public async Task<ProjectUserDTO> GetById(int id)
        {
            HttpResponseMessage response = await HttpClient.GetAsync($"ProjectUsers/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            string jsonString = await response.Content.ReadAsStringAsync();
            ProjectUserDTO projectUser = JsonConvert.DeserializeObject<ProjectUserDTO>(jsonString);
            return projectUser;
        }

        public async Task<List<ProjectUserDTO>> GetAll()
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

        public async Task<ProjectUserDTO> Create(ProjectUserDTO newProjectUser)
        {
            string jsonString = JsonConvert.SerializeObject(newProjectUser);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await HttpClient.PostAsync("ProjectUsers", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            jsonString = await response.Content.ReadAsStringAsync();
            ProjectUserDTO projectUser = JsonConvert.DeserializeObject<ProjectUserDTO>(jsonString);
            return projectUser;
        }

        public async Task<ProjectUserDTO> Update(ProjectUserDTO projectUserToUpdate)
        {
            string jsonString = JsonConvert.SerializeObject(projectUserToUpdate);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await HttpClient.PutAsync("ProjectUsers", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            jsonString = await response.Content.ReadAsStringAsync();
            ProjectUserDTO projectUser = JsonConvert.DeserializeObject<ProjectUserDTO>(jsonString);
            return projectUser;
        }

        public async Task<bool> DeleteById(int id)
        {
            HttpResponseMessage response = await HttpClient.DeleteAsync($"ProjectUsers/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            return true;
        }
    }
}
