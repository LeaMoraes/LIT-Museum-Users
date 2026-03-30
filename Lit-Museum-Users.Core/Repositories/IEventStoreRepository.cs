using Lit_Museum_Users.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lit_Museum_Users.Domain.Repositories
{
    public interface IEventStoreRepository
    {
        Task SaveEventsAsync(string aggregateId, string aggregateType, IEnumerable<IDomainEvent> events, int expectedVersion);
        Task<IEnumerable<IDomainEvent>> GetEventsAsync(string aggregateId);
        Task<IEnumerable<IDomainEvent>> GetEventsAsync(string aggregateId, int fromVersion);
    }
}
