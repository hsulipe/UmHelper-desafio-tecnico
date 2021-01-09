using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; private set; }
        public DateTime CreationDate { get; private set; }

        public EntityBase()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
    }
}
