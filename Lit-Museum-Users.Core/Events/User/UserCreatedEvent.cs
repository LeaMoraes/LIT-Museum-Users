using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lit_Museum_Users.Domain.Events.User
{
    public class UserCreatedEvent : DomainEvent
    {

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Role { get; private set; }

        public UserCreatedEvent(string name, string email, string role) : base(1)
        {
            Name = name;
            Email = email;
            Role = role;
        }
    }
}
