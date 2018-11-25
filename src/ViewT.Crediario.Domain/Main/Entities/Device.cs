using System;
using ViewT.Crediario.Domain.Core.Models;
using ViewT.Crediario.Domain.Main.Enums;

namespace ViewT.Crediario.Domain.Main.Entities
{
    public class Device : Entity
    {
        protected Device() { }

        public Device(Guid deviceId, string description, string deviceToken, string pushToken, string simCardNumber, DeviceOs deviceOs, string identification, Person person)
        {
            DeviceId = deviceId;
            Description = description;
            DeviceToken = deviceToken;
            PushToken = pushToken;
            SimCardNumber = simCardNumber;
            Identification = identification;
            DeviceOs = deviceOs;
            DeviceStatus = DeviceStatus.Active;
            Person = person;
            Active = true;
            DateCreated = DateTime.Now;
        }

        public Guid DeviceId { get; private set; }
        public string Description { get; private set; }
        public string DeviceToken { get; private set; }
        public string PushToken { get; private set; }
        public string SimCardNumber { get; private set; }
        public string Identification { get; private set; }
        public DeviceOs DeviceOs { get; private set; }
        public Guid PersonId { get; private set; }
        public Person Person { get; private set; }
        public DeviceStatus DeviceStatus { get; private set; }

        public void Enable()
        {
            DeviceStatus = DeviceStatus.Active;
        }

        public void Disable()
        {
            DeviceStatus = DeviceStatus.Inactive;
        }

        public void Activate()
        {
            Active = true;
        }
        public void Deactivate()
        {
            Active = false;
        }
    }
}