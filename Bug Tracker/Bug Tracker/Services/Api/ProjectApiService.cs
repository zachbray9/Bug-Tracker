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
    public class ProjectApiService : IProjectApiService
    {
        private readonly IHttpClientFactory HttpClientFactory;
        private readonly HttpClient HttpClient;

        public ProjectApiService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;

            HttpClient = HttpClientFactory.CreateClient();
            HttpClient.BaseAddress = new Uri("https://agileproapi.azurewebsites.net/api/");
            //HttpClient.BaseAddress = new Uri("https://localhost:7226/api/");
        }


        public async Task<ProjectDTO> GetByIdAsync(Guid id)
        {
            HttpResponseMessage response = await HttpClient.GetAsync($"Projects/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            string jsonString = await response.Content.ReadAsStringAsync();
            ProjectDTO project = JsonConvert.DeserializeObject<ProjectDTO>(jsonString);
            return project;
        }

        public async Task<List<ProjectDTO>> GetAllAsync()
        {
            HttpResponseMessage response = await HttpClient.GetAsync("Projects");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            string jsonString = await response.Content.ReadAsStringAsync();
            List<ProjectDTO> projects = JsonConvert.DeserializeObject<List<ProjectDTO>>(jsonString);
            return projects;
        }

        public async Task<List<ProjectUserDTO>> GetAllUsersOnProject(Guid projectId)
        {
            HttpResponseMessage response = await HttpClient.GetAsync($"Projects/{projectId}/Users");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString() );
            }

            string jsonString = await response.Content.ReadAsStringAsync();
            List<ProjectUserDTO> projectUsers = JsonConvert.DeserializeObject<List<ProjectUserDTO>>(jsonString);
            return projectUsers;
        }

        public async Task<List<TicketDTO>> GetAllTicketsOnProject(Guid projectId)
        {
            HttpResponseMessage response = await HttpClient.GetAsync($"Projects/{projectId}/Tickets");
            if(!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            string jsonString = await response.Content.ReadAsStringAsync();
            List<TicketDTO> tickets = JsonConvert.DeserializeObject<List<TicketDTO>>(jsonString);
            return tickets;
        }

        public async Task<ProjectDTO> CreateAsync(ProjectDTO entity)
        {
            string jsonString = JsonConvert.SerializeObject(entity);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await HttpClient.PostAsync("Projects", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            jsonString = await response.Content.ReadAsStringAsync();
            ProjectDTO project = JsonConvert.DeserializeObject<ProjectDTO>(jsonString);
            return project;
        }

        public async Task<ProjectDTO> UpdateAsync(Guid id, ProjectDTO entity)
        {
            string jsonString = JsonConvert.SerializeObject(entity);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await HttpClient.PutAsync($"Projects/{id}", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            jsonString = await response.Content.ReadAsStringAsync();
            ProjectDTO project = JsonConvert.DeserializeObject<ProjectDTO>(jsonString);
            return project;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            HttpResponseMessage response = await HttpClient.DeleteAsync($"Projects/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            return true;
        }
    }
}
