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
    public class TicketApiService : ITicketApiService
    {
        private readonly IHttpClientFactory HttpClientFactory;
        private readonly HttpClient HttpClient;

        public TicketApiService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;

            HttpClient = HttpClientFactory.CreateClient();
            //HttpClient.BaseAddress = new Uri("https://bugtrackerapi.azurewebsites.net/api/");
            HttpClient.BaseAddress = new Uri("https://localhost:7226/api/");
        }

        public async Task<TicketDTO> GetById(int id)
        {
            HttpResponseMessage response = await HttpClient.GetAsync($"Tickets/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            string jsonString = await response.Content.ReadAsStringAsync();
            TicketDTO ticket = JsonConvert.DeserializeObject<TicketDTO>(jsonString);
            return ticket;
        }

        public async Task<List<TicketDTO>> GetAll()
        {
            HttpResponseMessage response = await HttpClient.GetAsync("Tickets");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            string jsonString = await response.Content.ReadAsStringAsync();
            List<TicketDTO> tickets = JsonConvert.DeserializeObject<List<TicketDTO>>(jsonString);
            return tickets;
        }

        public async Task<List<CommentDTO>> GetAllCommentsOnTicket(int ticketId)
        {
            throw new NotImplementedException();
        }

        public async Task<TicketDTO> Create(TicketDTO newTicket)
        {
            string jsonString = JsonConvert.SerializeObject(newTicket);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await HttpClient.PostAsync("Tickets", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            jsonString = await response.Content.ReadAsStringAsync();
            TicketDTO ticket = JsonConvert.DeserializeObject<TicketDTO>(jsonString);
            return ticket;
        }

        public async Task<TicketDTO> Update(TicketDTO ticketToUpdate)
        {
            string jsonString = JsonConvert.SerializeObject(ticketToUpdate);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await HttpClient.PutAsync("Tickets", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            jsonString = await response.Content.ReadAsStringAsync();
            TicketDTO ticket = JsonConvert.DeserializeObject<TicketDTO>(jsonString);
            return ticket;
        }

        public async Task<bool> DeleteById(int id)
        {
            HttpResponseMessage response = await HttpClient.DeleteAsync($"Tickets/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            return true;
        }

    }
}
