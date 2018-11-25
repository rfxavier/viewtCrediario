using ViewT.Crediario.Domain.Main.Entities;

namespace ViewT.Crediario.Domain.Main.Interfaces
{
    public interface IDeviceRepository
    {
        Device GetByPerson(Person person);
        Device Add(Device newDevice);
        Device Update(Device device);
        Device GetByIdentification(string identification);
    }
}