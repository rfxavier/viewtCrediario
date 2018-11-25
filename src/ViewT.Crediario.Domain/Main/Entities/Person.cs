using System;
using System.Collections.Generic;
using ViewT.Crediario.Domain.Core.Models;
using ViewT.Crediario.Domain.Main.Enums;

namespace ViewT.Crediario.Domain.Main.Entities
{
    public class Person : Entity
    {
        protected Person() { }

        private readonly IList<Device> _deviceList;

        public Person(Guid personId, string name, string documentNumber, string phoneNumber, string email, string password, string serialKey)
        {
            PersonId = personId;
            Name = name;
            DocumentNumber = documentNumber;
            PhoneNumber = phoneNumber;
            Email = email;
            Password = password;
            SerialKey = serialKey;

            IsBlocked = false;
            Admin = false;
            Visitor = true;
            Resident = false;

            PersonUserStatus = PersonUserStatus.Active;
            Active = true;
            DateCreated = DateTime.Now;

            _deviceList = new List<Device>();
        }

        public Guid PersonId { get; private set; }
        public string Name { get; private set; }
        public string DocumentNumber { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string SerialKey { get; private set; }

        public bool IsBlocked { get; private set; }
        public bool Admin { get; private set; }
        public bool Visitor { get; private set; }
        public bool Resident { get; private set; }
        public string AddressBlock { get; private set; }
        public string AddressApartment { get; private set; }
        public string AddressComplement { get; private set; }
        public Token Token { get; private set; }
        public PersonUserStatus PersonUserStatus { get; private set; }



        public ICollection<Device> Devices
        {
            get { return _deviceList; }
        }

        public void AddDevice(Device device)
        {
            _deviceList.Add(device);
        }

        public void SetToken(Token token)
        {
            Token = token;
        }

        public void SetPassword(string password)
        {
            Password = password;
        }

        public void SetAddress(string addressBlock, string addressApartment, string addressComplement)
        {
            AddressBlock = addressBlock;
            AddressApartment = addressApartment;
            AddressComplement = addressComplement;
        }
        public void EnableUser()
        {
            PersonUserStatus = PersonUserStatus.Active;
        }

        public void DisableUser()
        {
            PersonUserStatus = PersonUserStatus.Inactive;
        }

        public void UpdateProfile(string name, string documentNumber, string phoneNumber, string email)
        {
            Name = name;
            DocumentNumber = documentNumber;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public void SetSerialKey(string serialKey)
        {
            SerialKey = serialKey;
        }

        public void SetAdminProfile()
        {
            Admin = true;
        }

        public void RevokeAdminProfile()
        {
            Admin = false;
        }
    }
}