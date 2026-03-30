using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lit_Museum_Users.Domain.Events.User
{
    public class UserUpdatedEvent : DomainEvent
    {

        public string Name { get; private set; }
        public string Email { get; private set; }

        public UserUpdatedEvent(string name, string email) : base(1)
        {
            Name = name;
            Email = email;
        }
    }
}
