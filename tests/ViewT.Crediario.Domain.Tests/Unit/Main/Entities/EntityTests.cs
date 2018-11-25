using FluentAssertions;
using System;
using ViewT.Condominio.Domain.Main.Entities;
using ViewT.Condominio.Domain.Main.Enums;
using ViewT.Condominio.Domain.Tests.Unit.Main.Entities.Builders;
using Xunit;
using Version = ViewT.Condominio.Domain.Main.Entities.Version;

namespace ViewT.Condominio.Domain.Tests.Unit.Main.Entities
{
    public class EntityTests
    {
        [Fact(DisplayName = "Version Constructor Active")]
        [Trait("Category", "Version")]
        public void Version_Constructor_ShouldInstantiateActiveTrue()
        {
            //Arrange
            Version version = null;

            //Act
            version = new Version(Guid.NewGuid(), DeviceOs.Android, 1);

            //Assert
            version.Active.Should().BeTrue();
        }

        [Fact(DisplayName = "Device Constructor Active")]
        [Trait("Category", "Device")]
        public void Device_Constructor_ShouldInstantiateActiveTrue()
        {
            //Arrange
            Device device = null;

            //Act
            device = new Device(Guid.Empty, string.Empty, string.Empty, string.Empty, string.Empty, null, string.Empty, null);

            //Assert
            device.Active.Should().BeTrue();
        }

        [Fact(DisplayName = "Device Constructor DeviceStatus")]
        [Trait("Category", "Device")]
        public void Device_Constructor_ShouldInstantiateDeviceStatusAsActive()
        {
            //Arrange
            Device device = null;

            //Act
            device = new Device(Guid.Empty, string.Empty, string.Empty, String.Empty, string.Empty, DeviceOs.iOS, string.Empty, null);

            //Assert
            device.DeviceStatus.Should().Be(DeviceStatus.Active);
        }


        [Fact(DisplayName = "Device Constructor Person")]
        [Trait("Category", "Device")]
        public void Device_Constructor_ShouldInstantiateUserAsNull()
        {
            //Arrange
            Device device = null;

            //Act
            device = new Device(Guid.Empty, string.Empty, string.Empty, String.Empty, string.Empty, DeviceOs.iOS, string.Empty, null);

            //Assert
            device.Person.Should().BeNull();
        }


        //[Fact(DisplayName = "Person Constructor Active")]
        //[Trait("Category", "Person")]
        //public void User_Constructor_ShouldInstantiateActiveTrue()
        //{
        //    //Arrange
        //    Person user = null;

        //    //Act
        //    user = new Person(Guid.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, false, false, false, Guid.Empty, null, String.Empty);

        //    //Assert
        //    user.Active.Should().BeTrue();
        //}

        //[Fact(DisplayName = "Person Constructor PersonUserStatus")]
        //[Trait("Category", "Person")]
        //public void User_Constructor_ShouldInstantiateUserStatusAsActive()
        //{
        //    //Arrange
        //    Person user = null;

        //    //Act
        //    user = new Person(Guid.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, false, false, false, Guid.Empty, null, String.Empty);

        //    //Assert
        //    user.PersonUserStatus.Should().Be(PersonUserStatus.Active);
        //}


        [Fact(DisplayName = "Token Constructor Active")]
        [Trait("Category", "Token")]
        public void Token_Constructor_ShouldInstantiateActiveTrue()
        {
            //Arrange
            Token version = null;

            //Act
            version = new Token(Guid.Empty, Guid.Empty, DeviceOs.iOS);

            //Assert
            version.Active.Should().BeTrue();
        }

        [Fact(DisplayName = "EmailNotification Constructor Sent")]
        [Trait("Category", "Token")]
        public void Token_Constructor_ShouldInstantiateSentFalse()
        {
            //Arrange
            EmailNotification emailNotification = null;

            //Act
            emailNotification = new EmailNotification(Guid.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, DateTime.MinValue);

            //Assert
            emailNotification.Sent.Should().BeFalse();
        }
    }
}