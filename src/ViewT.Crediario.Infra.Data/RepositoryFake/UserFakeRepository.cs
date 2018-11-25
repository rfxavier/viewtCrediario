using System;
using ViewT.Crediario.Domain.Main.Entities;
using ViewT.Crediario.Domain.Main.Interfaces;
using ViewT.Crediario.Domain.Tests.Unit.Main.Entities.Builders;

namespace ViewT.Crediario.Infra.Data.RepositoryFake
{
    public class UserFakeRepository : IPersonRepository
    {
        public Person Add(Person person)
        {
            return new PersonBuilder()
                .WithPersonId(Guid.NewGuid());
        }

        public Person GetByEmail(string email)
        {
            var user = new PersonBuilder().WithSerialKey("").WithPersonId(Guid.NewGuid());
            return user;
        }

        public Person GetByUserNameAndPassword(string userName, string userPassword)
        {
            Person user = null;
            switch (userName) {
                case "admin@viewt.com.br" :
                    user = new PersonBuilder()
                        .WithPersonId(Guid.NewGuid())
                        .WithEmail("admin@viewt.com.br")
                        .WithName("Administrador")
                        .WithSerialKey("")
                        .WithPhoneNumber("1933221100")
                        .WithDocumentNumber("443333337")
                        .WithAdmin(true);
                    break;
                case "visitor@viewt.com.br":
                    user = new PersonBuilder()
                        .WithPersonId(Guid.NewGuid())
                        .WithEmail("visitor@viewt.com.br")
                        .WithName("Visitante")
                        .WithSerialKey("")
                        .WithPhoneNumber("1933221100")
                        .WithDocumentNumber("443333337")
                        .WithVisitor(true);
                    break;
                case "resident@viewt.com.br":
                    user = new PersonBuilder()
                        .WithPersonId(Guid.NewGuid())
                        .WithEmail("resident@viewt.com.br")
                        .WithName("Morador")
                        .WithSerialKey("")
                        .WithPhoneNumber("1933221100")
                        .WithDocumentNumber("443333337")
                        .WithResident(true);
                    break;
                case "adminresident@viewt.com.br":
                    user = new PersonBuilder()
                        .WithPersonId(Guid.NewGuid())
                        .WithEmail("adminresident@viewt.com.br")
                        .WithName("Administrador e Morador")
                        .WithSerialKey("")
                        .WithPhoneNumber("1933221100")
                        .WithDocumentNumber("443333337")
                        .WithAdmin(true)
                        .WithResident(true);
                    break;
                default:
                    user = new PersonBuilder()
                        .WithPersonId(Guid.NewGuid())
                        .WithEmail("visitor@viewt.com.br")
                        .WithName("Visitante")
                        .WithSerialKey("")
                        .WithPhoneNumber("1933221100")
                        .WithDocumentNumber("443333337")
                        .WithVisitor(true);
                    break;
            }

            return user;
        }

        public Person Update(Person person)
        {
            return person;
        }

        public Person GetBySerialKey(string serialKey)
        {
            var user = new PersonBuilder().WithPersonId(new Guid("cf0c2a93-e3c6-4e53-b243-960213d745a6"));
            return user;
        }
    }
}