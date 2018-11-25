using System;
using System.Collections.Generic;
using System.Linq;

namespace ViewT.Crediario.Domain.Main.Enums
{
    public class DeviceStatus
    {
        public static DeviceStatus Active { get; } = new DeviceStatus(1, "Active");
        public static DeviceStatus Inactive { get; } = new DeviceStatus(2, "Inactive");

        private DeviceStatus(int val, string name)
        {
            Value = val;
            Name = name;
        }

        private DeviceStatus() { } //required for EF

        public int Value { get; private set; }
        public string Name { get; private set; }

        public static IEnumerable<DeviceStatus> List()
        {
            return new[] { Active, Inactive };
        }

        public static DeviceStatus FromString(string enumString)
        {
            return List().FirstOrDefault(r => String.Equals(r.Name, enumString, StringComparison.OrdinalIgnoreCase));
        }

        public static DeviceStatus FromValue(int value)
        {
            return List().FirstOrDefault(r => r.Value == value);
        }

    }
}