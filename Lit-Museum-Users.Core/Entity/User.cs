using Lit_Museum_Users.Core.Events;
using Lit_Museum_Users.Core.Events.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lit_Museum_Users.Core.Entity
{
    public class User : EntityBase
    {
        /// <summary>
        /// Nome do Usuario
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email do Usuario
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Senha do Usuario Criptografada
        /// </summary>
        public string PasswordHash { get; set; }
        /// <summary>
        /// Nivel de Acesso do Usuario
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// Flag indicando se o usuario foi deletado
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        private readonly List<IDomainEvent> _uncommitedEvents = new List<IDomainEvent>();
        public int Version { get; set; } = 0;
        public IEnumerable<IDomainEvent> GetUncommittedEvents() => _uncommitedEvents.AsReadOnly();
        public void MarkEventsAsCommitted()
        {
            _uncommitedEvents.Clear();
        }

        public void Apply(UserCreatedEvent @event)
        {
            Name = @event.Name;
            Email = @event.Email;
            Role = @event.Role;

            _uncommitedEvents.Add(@event);
        }

        public void Apply(UserUpdatedEvent @event)
        {
            Name = @event.Name;
            Email = @event.Email;
            UpdatedAt = DateTime.UtcNow;

            _uncommitedEvents.Add(@event);
        }

        public void Apply(UserPasswordChangedEvent @event)
        {
            UpdatedAt = DateTime.UtcNow;

            _uncommitedEvents.Add(@event);
        }

        public void Apply(UserDeletedEvent @event)
        {
            IsDeleted = true;

            _uncommitedEvents.Add(@event);
        }

        public void Apply(UserRoleChangedEvent @event)
        {
            Role = @event.Role;
            UpdatedAt = DateTime.UtcNow;

            _uncommitedEvents.Add(@event);
        }

    }
}
