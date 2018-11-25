using System;
using ViewT.Crediario.Domain.Main.Entities;
using ViewT.Crediario.Domain.Main.Interfaces;
using ViewT.Crediario.Domain.Tests.Unit.Main.Entities.Builders;

namespace ViewT.Crediario.Infra.Data.RepositoryFake
{
    public class DeviceFakeRepository : IDeviceRepository
    {
        public Device GetByPerson(Person person)
        {
            return new DeviceBuilder()
                .WithPerson(new PersonBuilder().WithPersonId(new Guid("cf0c2a93-e3c6-4e53-b243-960213d745a6")));
        }

        public Device Add(Device newDevice)
        {
            return new DeviceBuilder().WithDeviceId(Guid.NewGuid());
        }

        public Device Update(Device device)
        {
            return new DeviceBuilder().WithDeviceId(Guid.NewGuid());
        }

        public Device GetByIdentification(string identification)
        {
            return new DeviceBuilder().WithDeviceId(Guid.NewGuid()).WithPerson(new PersonBuilder().WithPersonId(new Guid("cf0c2a93-e3c6-4e53-b243-960213d745a6")));
        }
    }
}