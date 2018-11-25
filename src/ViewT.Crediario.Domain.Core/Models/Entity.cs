using System;

namespace ViewT.Crediario.Domain.Core.Models
{
    public abstract class Entity
    {
        public DateTime DateCreated { get; protected set; }
        public bool Active { get; protected set; }
    }
}