﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Models
{
    public class User : DomainObject
    {
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public ICollection<ProjectUser> ProjectUsers { get; set; } = null!;
        public DateTime DateJoined { get; set; }

        public User()
        {
            ProjectUsers = new List<ProjectUser>();
        }

        public override string ToString()
        {
            return Email;
        }
    }
}
