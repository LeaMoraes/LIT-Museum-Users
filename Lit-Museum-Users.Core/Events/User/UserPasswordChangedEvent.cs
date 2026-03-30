using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lit_Museum_Users.Domain.Events.User
{
    public class UserPasswordChangedEvent : DomainEvent
    {

        public UserPasswordChangedEvent() : base(1) { }

    }
}
