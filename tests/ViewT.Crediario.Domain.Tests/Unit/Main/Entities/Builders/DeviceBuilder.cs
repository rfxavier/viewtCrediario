using System;
using ViewT.Crediario.Domain.Main.Entities;
using ViewT.Crediario.Domain.Main.Enums;

namespace ViewT.Crediario.Domain.Tests.Unit.Main.Entities.Builders
{
    public class DeviceBuilder
    {
        private Guid _deviceId = Guid.Empty;
        private string _description = null;
        private string deviceToken = null;
        private string pushToken = null;
        private string simCardNumber = null;
        private DeviceOs _os = DeviceOs.iOS;
        private string _identification = null;
        private DeviceStatus _deviceStatus = null;
        private Person _user = null;
        private bool _active = false;


        public Device Build()
        {
            var device = new Device(_deviceId, _description, deviceToken, pushToken, simCardNumber, _os, _identification, _user);

            if (_deviceStatus == DeviceStatus.Active)
            {
                device.Enable();
            }
            else if (_deviceStatus == DeviceStatus.Inactive)
            {
                device.Disable();
            }

            if (_active == true)
            {
                device.Activate();
            }
            else if (_active == false)
            {
                device.Deactivate();
            }

            return device;
        }

        public DeviceBuilder WithDeviceId(Guid deviceId)
        {
            this._deviceId = deviceId;
            return this;
        }

        public DeviceBuilder WithDescription(string description)
        {
            this._description = description;
            return this;
        }

        public DeviceBuilder WithDeviceOs(DeviceOs os)
        {
            this._os = os;
            return this;
        }

        public DeviceBuilder WithIdentification(string identification)
        {
            this._identification = identification;
            return this;
        }

        public DeviceBuilder WithDeviceStatus(DeviceStatus deviceStatus)
        {
            this._deviceStatus = deviceStatus;
            return this;
        }

        public DeviceBuilder WithPerson(Person person)
        {
            this._user = person;
            return this;
        }

        public DeviceBuilder WithActive(bool active)
        {
            this._active = active;
            return this;
        }


        public static implicit operator Device(DeviceBuilder instance)
        {
            return instance.Build();
        }
    }
}