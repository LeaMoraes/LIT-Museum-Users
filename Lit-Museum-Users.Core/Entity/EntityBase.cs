using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lit_Museum_Users.Domain.Entity
{
    public class EntityBase
    {
        /// <summary>
        /// Identificador Unico
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Data de Criação
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Data da ultima atualização
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        public EntityBase() { CreatedAt = DateTime.UtcNow; }

    }
}
