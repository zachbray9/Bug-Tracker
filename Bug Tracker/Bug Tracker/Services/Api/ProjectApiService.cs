﻿using BugTracker.Domain.Models.DTOs;
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
    public class ProjectApiService : IApiService<ProjectDTO>
    {
        private readonly IHttpClientFactory HttpClientFactory;
        private readonly HttpClient HttpClient;

        public ProjectApiService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;

            HttpClient = HttpClientFactory.CreateClient();
            HttpClient.BaseAddress = new Uri("https://bugtrackerapi.azurewebsites.net/api/");
        }

        public async Task<ProjectDTO> GetById(int id)
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

        public async Task<List<ProjectDTO>> GetAll()
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

        public async Task<ProjectDTO> Create(ProjectDTO newProject)
        {
            string jsonString = JsonConvert.SerializeObject(newProject);
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

        public async Task<ProjectDTO> Update(ProjectDTO projectToUpdate)
        {
            string jsonString = JsonConvert.SerializeObject(projectToUpdate);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await HttpClient.PutAsync("Projects", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            jsonString = await response.Content.ReadAsStringAsync();
            ProjectDTO project = JsonConvert.DeserializeObject<ProjectDTO>(jsonString);
            return project;
        }

        public async Task<bool> DeleteById(int id)
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
