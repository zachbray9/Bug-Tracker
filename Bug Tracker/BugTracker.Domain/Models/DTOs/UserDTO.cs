using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Models.DTOs
{
    public class UserDTO
    {
        public int Id {  get; set; }
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName { get => $"{FirstName} {LastName}"; }
        public string Initials { get => $"{FirstName?.FirstOrDefault()}{LastName?.FirstOrDefault()}".ToUpper(); }
        public string PasswordHash { get; set; } = null!;
        public ICollection<ProjectUserDTO> ProjectUsers { get; set; } = null!;
        public DateTime DateJoined { get; set; }

        public UserDTO()
        {
            ProjectUsers = new List<ProjectUserDTO>();
        }

        public override string ToString()
        {
            return Email;
        }
    }
}
