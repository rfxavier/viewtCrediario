using System;
using System.Collections.Generic;
using System.Linq;

namespace ViewT.Crediario.Domain.Main.Enums
{
    public class PersonUserStatus
    {
        public static PersonUserStatus Active { get; } = new PersonUserStatus(1, "Active");
        public static PersonUserStatus Inactive { get; } = new PersonUserStatus(2, "Inactive");

        private PersonUserStatus(int val, string name)
        {
            Value = val;
            Name = name;
        }

        private PersonUserStatus() { } //required for EF

        public int Value { get; private set; }
        public string Name { get; private set; }

        public static IEnumerable<PersonUserStatus> List()
        {
            return new[] { Active, Inactive };
        }

        public static PersonUserStatus FromString(string enumString)
        {
            return List().FirstOrDefault(r => String.Equals(r.Name, enumString, StringComparison.OrdinalIgnoreCase));
        }

        public static PersonUserStatus FromValue(int value)
        {
            return List().FirstOrDefault(r => r.Value == value);
        }
    }
}