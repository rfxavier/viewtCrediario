using ViewT.Crediario.Domain.Main.Entities;

namespace ViewT.Crediario.Domain.Main.Interfaces
{
    public interface IPersonRepository
    {
        Person Add(Person person);
        Person GetByEmail(string email);
        Person GetByUserNameAndPassword(string userName, string userPassword);
        Person Update(Person person);
        Person GetBySerialKey(string serialKey);
    }
}