using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lit_Museum_Users.Core.Events.User
{
    public class UserDeletedEvent : DomainEvent
    {

        public UserDeletedEvent() : base(1) { }

    }
}
