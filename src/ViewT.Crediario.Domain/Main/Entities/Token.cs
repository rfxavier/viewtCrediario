using System;
using ViewT.Crediario.Domain.Core.Models;
using ViewT.Crediario.Domain.Main.Enums;

namespace ViewT.Crediario.Domain.Main.Entities
{
    public class Token : Entity
    {
        protected Token() { }
        public Token(Guid tokenId, Guid userToken, DeviceOs deviceOs)
        {
            TokenId = tokenId;
            UserToken = userToken;
            DeviceOs = deviceOs;

            Person = null;
            Active = true;
            DateCreated = DateTime.Now;
        }

        public Guid TokenId { get; private set; }
        public Guid UserToken { get; private set; }
        public DeviceOs DeviceOs { get; private set; }
        public Person Person { get; private set; }

        public void Deactivate()
        {
            Active = false;
        }

        public void Activate()
        {
            Active = true;
        }
    }
}