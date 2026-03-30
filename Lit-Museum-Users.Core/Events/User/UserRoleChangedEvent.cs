using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lit_Museum_Users.Domain.Events.User
{
    public class UserRoleChangedEvent : DomainEvent
    {

        public string Role { get; private set; }

        public UserRoleChangedEvent(string role) :base(1)
        {
            Role = role;
        }

    }
}
