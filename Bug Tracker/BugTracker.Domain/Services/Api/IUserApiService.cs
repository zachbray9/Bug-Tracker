using BugTracker.Domain.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Services.Api
{
    public interface IUserApiService : IApiService<UserDTO>
    {
        Task<UserDTO> GetByEmail(string email);
        Task<UserDTO> GetByFullName(string fullName);
        Task<List<ProjectDTO>> GetAllProjectsFromUserById(int id);
    }
}
