using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lit_Museum_Users.Domain.Events
{
    public interface IDomainEvent
    {

        Guid Id { get; }
        DateTime OccurredOn { get; }
        string EventType { get; }
        int Version { get; }

    }
}
