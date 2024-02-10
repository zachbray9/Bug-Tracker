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
    public class UserApiService : IUserApiService
    {
        private readonly IHttpClientFactory HttpClientFactory;
        private readonly HttpClient HttpClient;

        public UserApiService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;

            HttpClient = HttpClientFactory.CreateClient();
            //HttpClient.BaseAddress = new Uri("https://bugtrackerapi.azurewebsites.net/api/");
            HttpClient.BaseAddress = new Uri("https://localhost:7226/api/");
        }

        public async Task<UserDTO> GetById(int id)
        {
            HttpResponseMessage response = await HttpClient.GetAsync($"Users/{id}");
            if(!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            string jsonString = await response.Content.ReadAsStringAsync();
            UserDTO user = JsonConvert.DeserializeObject<UserDTO>(jsonString);
            return user;
        }

        public async Task<UserDTO> GetByEmail(string email)
        {
            HttpResponseMessage response = await HttpClient.GetAsync($"Users/byEmail/{email}");
            if (!response.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine(response.StatusCode.ToString());
                return null;
            }

            string jsonString = await response.Content.ReadAsStringAsync();
            UserDTO user = JsonConvert.DeserializeObject<UserDTO>(jsonString);
            return user;
        }

        public async Task<UserDTO> GetByFullName(string fullName)
        {
            HttpResponseMessage response = await HttpClient.GetAsync($"Users/byName/{fullName}");
            if (!response.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine(response.StatusCode.ToString());
                return null;
            }

            string jsonString = await response.Content.ReadAsStringAsync();
            UserDTO user = JsonConvert.DeserializeObject<UserDTO>(jsonString);
            return user;
        }

        public async Task<List<UserDTO>> GetAll()
        {
            HttpResponseMessage response = await HttpClient.GetAsync("Users");
            if (!response.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine(response.StatusCode.ToString());
                return new List<UserDTO>();
            }

            string jsonString = await response.Content.ReadAsStringAsync();
            List<UserDTO> users = JsonConvert.DeserializeObject<List<UserDTO>>(jsonString);
            return users;
        }

        public async Task<List<ProjectDTO>> GetAllProjectsFromUserById(int id)
        {
            HttpResponseMessage response = await HttpClient.GetAsync($"Users/{id}/Projects");
            if (!response.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine(response.StatusCode.ToString());
                return new List<ProjectDTO>();
            }

            string jsonString = await response.Content.ReadAsStringAsync();
            List<ProjectDTO> projects = JsonConvert.DeserializeObject<List<ProjectDTO>>(jsonString);
            return projects;
        }

        public async Task<UserDTO> Create(UserDTO newUser)
        {
            string jsonString = JsonConvert.SerializeObject(newUser);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await HttpClient.PostAsync("Users", content);
            if(!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            jsonString = await response.Content.ReadAsStringAsync();
            UserDTO user = JsonConvert.DeserializeObject<UserDTO>(jsonString);
            return user;
        }

        public async Task<UserDTO> Update(UserDTO userToUpdate)
        {
            string jsonString = JsonConvert.SerializeObject(userToUpdate);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await HttpClient.PutAsync("Users", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            jsonString = await response.Content.ReadAsStringAsync();
            UserDTO user = JsonConvert.DeserializeObject<UserDTO>(jsonString);
            return user;
        }

        public async Task<bool> DeleteById(int id)
        {
            HttpResponseMessage response = await HttpClient.DeleteAsync($"Users/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            return true;
        }

    }
}
